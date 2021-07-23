function LoadAreaListFromDB() {
    try {
        arrayAreaList = [];
        $.ajax({
            url: '/AreaEditor/LoadAreaListFromDB',
            data: {},
            type: 'POST',
            success: function (data) {
                jsonAreaList = data;
                fillAreasFilter(data);
                //SetArrayOfPolygonNew([{ id: 535, parentid: 0, level: 3, name: "\u0425\u0430\u0431\u0020\u0032", rgbline: "\u0034\u0030\u0030\u0030\u0034\u0030", rgbfill: "\u0034\u0030\u0038\u0030\u0038\u0030", routenum: 535, latlngarr: [{ lat: 55.73231949152960, lng: 37.68554326146840 }, { lat: 55.73265122413170, lng: 37.68551560118800 }, { lat: 55.73265084576700, lng: 37.68573956564070 }, { lat: 55.73253241745730, lng: 37.68575968220830 }, { lat: 55.73233055878780, lng: 37.68577493727210 }], sessionid: "" }]);
                //SetBoundsForPoints([{ lat: 55.73231949152960, lng: 37.68554326146840 }, { lat: 55.73265122413170, lng: 37.68551560118800 }, { lat: 55.73265084576700, lng: 37.68573956564070 }, { lat: 55.73253241745730, lng: 37.68575968220830 }, { lat: 55.73233055878780, lng: 37.68577493727210 }])
            },
            error: function () { alert('Не удалось получить список зон'); }
        });
    }
    catch (e) {
        alert('Ошибка ' + e.name + ":" + e.message + "\n" + e.stack);
    }
}

function strEncodeUTF16(str) {
    let arr = []
    for (let i = 0; i < str.length; i++) {
        arr[i] = str.charCodeAt(i);
    }
    return arr;
}

function fillAreasFilter(dt) {
    /* заполнение вкладок в окне фильтра областей */
    let af_t1_tree = document.getElementById("af_t1_tree");
    let af_t2_tree = document.getElementById("af_t2_tree");
    let areaList = JSON.parse(dt).AreaList;
    let innerHTML = "";
    let areaId = -1;
    let area = null;
    let areaLevelEl = null;
    if (areaList.length > 0) {
        areaList.forEach(function (el) {
            if (areaId != el.ID) {
                areaId = el.ID;
                area = { id: el.ID, name: el.NAME, level: el.LEVEL, parent: el.PARENTID, routenum: el.ROUTENUM, fillColor: el.RGBFILL, lineColor: el.RGBLINE, points: [] };
                arrayAreaList.push(area);
                innerHTML += getAreasFilterElementHTMLText(1, el.ID, el.RGBFILL, el.RGBLINE, el.NAME);

                if (!document.getElementById("af_t3_level" + el.LEVEL + "det")) {
                    document.getElementById("af_t3_level" + el.LEVEL).innerHTML +=
                        '<details id = "af_t3_level' + el.LEVEL + 'det"><summary></summary></details>';
                }
                areaLevelEl = document.getElementById("af_t3_level" + el.LEVEL + "det");
                areaLevelEl.innerHTML += getAreasFilterElementHTMLText(3, el.ID, el.RGBFILL, el.RGBLINE, el.NAME);
            }
            if (area != null) area.points.push({ lat: el.LAT, lng: el.LNG });
        });
    }
    af_t1_tree.innerHTML = innerHTML;

    innerHTML = "";
    let parentNodes = [];
    let childNodes = [];
    arrayAreaList.forEach(function (el) {
        if (el.parent == 0) {
            innerHTML += getAreasFilterElementHTMLText(2, el.id, el.fillColor, el.lineColor, el.name);
            parentNodes.push(el.id);
        }
        else childNodes.push(el);
    });
    af_t2_tree.innerHTML = innerHTML;
    fillTreeChildNodes(parentNodes, childNodes);
    document.getElementById("cnt_areas_selected").innerText = "Всего зон: " + arrayAreaList.length + " / Выбрано: " + arraySelectedAreaList.length;
}

function fillTreeChildNodes(pNodes, cNodes) {
    let parentNodes = [];
    let childNodes = [];
    let elem = null;
    let elemDet = null;
    cNodes.forEach(function (el) {
        if (pNodes.indexOf(el.parent) != -1) {
            elem = document.getElementById("af_t2_el" + el.parent);
            if (!document.getElementById("af_t2_el" + el.parent + "det")) {
                elem.innerHTML += '<details id = "af_t2_el' + el.parent + 'det"><summary></summary></details>'
            }
            elemDet = document.getElementById("af_t2_el" + el.parent + "det");
            elemDet.innerHTML += getAreasFilterElementHTMLText(2, el.id, el.fillColor, el.lineColor, el.name);
            parentNodes.push(el.id);
        }
        else childNodes.push(el);
    });
    if (parentNodes.length > 0 && childNodes.length > 0) fillTreeChildNodes(parentNodes, childNodes);
}

function getAreasFilterElementHTMLText(tabN, elId, fillColor, lineColor, elName) {
    return '<div class="treeEl" id="af_t' + tabN + '_el' + elId + '"><input type="checkbox" id="chk_t' + tabN + '_' + elId +
        '" value="' + elId + '" onchange="changeAreasFilter(this,' + tabN + ')"/><p class="colorRectangle" style="background-color: #'
        + fillColor + '; border-color: #' + lineColor + '"></p>' + elId + ' | ' + elName + '</div>';
}

function changeAreasFilter(ths, tabN) {
    /* действие при щелчке на checkbox'е */
    changeSeletedAreasList(ths);
    if ((document.getElementById("areasFilterTab2").checked && document.getElementById("af_t2_inp1").checked) ||
        (document.getElementById("areasFilterTab3").checked && document.getElementById("af_t3_inp1").checked)) {
        if (tabN == 2 || tabN == 3) {
            let val = ths.value;
            let childInputs = [];
            getChildrensInputs(tabN, val, childInputs);
            childInputs.forEach(function (el) {
                let input = document.getElementById("chk_t" + tabN + "_" + el);
                input.checked = ths.checked;
                changeSeletedAreasList(input);
            });
            if (tabN == 2) {
                let parentArea = getParentArea(val);
                while (parentArea != 0) {
                    let parentInp = document.getElementById("chk_t" + tabN + "_" + parentArea);
                    let childrenAreas = getChildAreas(parentArea);
                    let childrenCheck = 0;
                    let inp = null;
                    childrenAreas.forEach(function (el) {
                        inp = document.getElementById("chk_t" + tabN + "_" + el);
                        if (inp.indeterminate) childrenCheck += 0.5;
                        else if (inp.checked) childrenCheck += 1;
                    });
                    if (childrenCheck == 0) {
                        parentInp.checked = false;
                        parentInp.indeterminate = false;
                    }
                    else if (childrenAreas.length == childrenCheck) {
                        parentInp.checked = true;
                        parentInp.indeterminate = false;
                    }
                    else if (childrenAreas.length > childrenCheck) parentInp.indeterminate = true;
                    changeSeletedAreasList(parentInp);
                    parentArea = getParentArea(parentArea);
                }
            }
            if (tabN == 3) {
                let parentLvl = null;
                for (let m = 0; m < arrayAreaList.length; m++) {
                    if (val == arrayAreaList[m].id.toString()) {
                        parentLvl = document.getElementById("af_t3_level" + arrayAreaList[m].level);
                        break;
                    }
                }
                if (parentLvl) {
                    let childInputs = parentLvl.getElementsByTagName("INPUT");
                    let cnt = childInputs.length;
                    let childrenCheck = 0;
                    let childInput = null;
                    for (let m = 0; m < childInputs.length; m++) {
                        if (childInputs[m].parentNode == parentLvl) {
                            childInput = childInputs[m];
                            cnt -= 1;
                            continue;
                        }
                        if (childInputs[m].checked) childrenCheck += 1;
                    }
                    if (childrenCheck == 0) {
                        childInput.checked = false;
                        childInput.indeterminate = false;
                    }
                    else if (childrenCheck == cnt) {
                        childInput.checked = true;
                        childInput.indeterminate = false;
                    }
                    else if (cnt > childrenCheck) {
                        childInput.checked = true;
                        childInput.indeterminate = true;
                    }
                }
            }
        }
    }
}

function getParentArea(id) {
    let parentId = 0;
    arrayAreaList.forEach(function (el) {
        if (el.id == id) {
            parentId = el.parent;
            return false;
        }
    });
    return parentId;
}

function getChildAreas(id) {
    let childAreas = [];
    arrayAreaList.forEach(function (el) {
        if (el.parent == id) childAreas.push(el.id);
    });
    return childAreas;
}

function getChildrensInputs(tabN, val, arrayOfChild) {
    /* получение массива дочерних checkbox'ов для текущего*/
    if (tabN == 2) {
        arrayAreaList.forEach(function (el) {
            if (el.parent == val) {
                arrayOfChild.push(el.id);
                getChildrensInputs(2, el.id, arrayOfChild);
            }
        });
    }
    else if (tabN == 3) {
        if (val.substring(0, 3) == 'lvl') {
            let valInt = parseInt(val.substring(3, val.length), 10);
            arrayAreaList.forEach(function (el) {
                if (el.level == valInt) arrayOfChild.push(el.id);
            });
        }
    } 
}

function changeSeletedAreasList(el) {
    /* изменяет массив выделенных зон */
    if (el.value.substring(0, 3) != 'lvl') {
        let pos = arraySelectedAreaList.indexOf(el.value);
        if ((el.checked || el.indeterminate) && pos == -1) arraySelectedAreaList.push(el.value);
        else if (!el.checked && !el.indeterminate && pos != -1) arraySelectedAreaList.splice(pos, 1);
        document.getElementById("cnt_areas_selected").innerText = "Всего зон: " + arrayAreaList.length
            + " / Выбрано: " + arraySelectedAreaList.length;
    }
}

function changeAreasFilterTab(tabN) {
    /* смена вкладки в окне фильтра зон */
    let allTabInputs = document.getElementById("af_t" + tabN + "_tree").getElementsByTagName("INPUT");
    for (let m = 0; m < allTabInputs.length; m++) {
        allTabInputs[m].checked = false;
        allTabInputs[m].indeterminate = false;
    }
    arraySelectedAreaList.forEach(function (el) {
        let inp = document.getElementById("chk_t" + tabN + "_" + el);
        let childInputs = [];
        getChildrensInputs(tabN, inp.value, childInputs);
        if (childInputs.length == 0) inp.checked = true;
        else {
            let cnt = 0;
            childInputs.forEach(function (el) {
                if (arraySelectedAreaList.indexOf(el.toString()) != -1) cnt += 1;
            });
            if (cnt == childInputs.length) {
                inp.checked = true;
                inp.indeterminate = false;
            }
            else {
                inp.checked = true;
                inp.indeterminate = true;
            }
        }
    });
    if (tabN == 3) {
        for (let m = 0; m < 5; m++) {
            let lvl = document.getElementById("af_t3_level" + m).getElementsByTagName("INPUT");
            let cnt = lvl.length;
            let cntSel = 0;
            let lvlInp = null
            for (let n = 0; n < lvl.length; n++) {
                if (lvl[n].value.substring(0, 3) == 'lvl') {
                    lvlInp = lvl[n];
                    cnt -= 1;
                }
                else if (arraySelectedAreaList.indexOf(lvl[n].value) != -1 ) cntSel += 1;
            }
            if (cntSel == 0) {
                lvlInp.checked = false;
                lvlInp.indeterminate = false;
            }
            else if (cnt == cntSel) {
                lvlInp.checked = true;
                lvlInp.indeterminate = false;
            }
            else if (cnt > cntSel) {
                lvlInp.checked = true;
                lvlInp.indeterminate = true;
            }
        }
    }
}

function selectAllAreas() {
    arraySelectedAreaList = [];
    arrayAreaList.forEach(function (el) {
        arraySelectedAreaList.push(el.id.toString());
    });
    updateSelections();
}

function unselectAllAreas() {
    arraySelectedAreaList = [];
    updateSelections();
}

function invertSelectAreas() {
    let tmp = arraySelectedAreaList;
    arraySelectedAreaList = [];
    arrayAreaList.forEach(function (el) {
        if (tmp.indexOf(el.id.toString()) == -1) arraySelectedAreaList.push(el.id.toString());
    });
    updateSelections();
}

function updateSelections() {
    if (document.getElementById("areasFilterTab1").checked) changeAreasFilterTab(1);
    else if (document.getElementById("areasFilterTab2").checked) changeAreasFilterTab(2);
    else if (document.getElementById("areasFilterTab3").checked) changeAreasFilterTab(3);
    document.getElementById("cnt_areas_selected").innerText = "Всего зон: " + arrayAreaList.length
        + " / Выбрано: " + arraySelectedAreaList.length;
}

function showSelectedAreas() {
    if (arraySelectedAreaList.length > 0) {
        let areas = [];
        let minLat = null;
        let maxLat = null;
        let minLon = null;
        let maxLon = null;
        arraySelectedAreaList.forEach(function (el) {
            let area = null;
            arrayAreaList.forEach(function (ael) {
                if (parseInt(el, 10) == ael.id) {
                    area = {
                        id: ael.id,
                        parentid: ael.parent,
                        level: ael.level,
                        name: ael.name,
                        rgbline: ael.lineColor,
                        rgbfill: ael.fillColor,
                        routenum: ael.routenum,
                        latlngarr: ael.points,
                        sessionid: ""
                    }
                    ael.points.forEach(function (pt) {
                        if (pt.lat > maxLat || maxLat == null) { maxLat = pt.lat };
                        if (pt.lat < minLat || minLat == null) { minLat = pt.lat };
                        if (pt.lng > maxLon || maxLon == null) { maxLon = pt.lng };
                        if (pt.lng < minLon || minLon == null) { minLon = pt.lng };
                    });
                    return false;
                } 12
            });
            if (area) areas.push(area);
        })
        let boundPoints = [{ lat: minLat, lng: minLon }, { lat: maxLat, lng: maxLon }];
        SetArrayOfPolygonNew(areas);
        SetBoundsForPoints(boundPoints);
    }
}

function findAllIntersections() {
    /* проверяет все области на пересечение */
    let intersectAreas = [];
    arrayAreaList.forEach(function (el) {
        let pts = [];
        el.points.forEach(function (ept) {
            let pt = [];
            pt.push(ept.lat);
            pt.push(ept.lng);
            pts.push(pt);
        });
        if (findInetrsection(pts)) intersectAreas.push({ id: el.id, name: el.name });
    });
    alert(JSON.stringify(intersectAreas));
}

function findInetrsection(aPts) {
    /* проверка на пересечение области */
    let bLat = aPts[0][0];
    let bLng = aPts[0][1];
    let lat = bLat;
    let lng = bLng;
    let lines = [];
    for (let m = 1; m < aPts.length; m++) {
        let line = new ymaps.Polyline([[lat, lng], [aPts[m][0], aPts[m][1]]]);
        lines.push({ line: line, begin: ("" + lat + lng), end: ("" + aPts[m][0] + aPts[m][1]) });
        lat = aPts[m][0];
        lng = aPts[m][1];
    }
    lines.push({ line: (new ymaps.Polyline([[lat, lng], [bLat, bLng]])), begin: ("" + lat + lng), end: ("" + bLat + bLng) });
    let hasIntersect = 0;
    lines.forEach(function (el) {
        map.geoObjects.add(el.line);
    });
    for (let m = 0; m < lines.length; m++) {
        for (let n = 0; n < lines.length; n++) {
            if (m != n) {
                if (lines[m].begin != lines[n].begin &&
                    lines[m].begin != lines[n].end &&
                    lines[m].end != lines[n].begin &&
                    lines[m].end != lines[n].end) {
                    let obj = ymaps.geoQuery(lines[m].line).searchIntersect(lines[n].line);
                    if (obj.getLength() > 0) hasIntersect += 1;
                }
            }
            if (hasIntersect > 0) break;
        }
        if (hasIntersect > 0) break;
    }
    lines.forEach(function (el) {
        map.geoObjects.remove(el.line);
    });
    if (hasIntersect > 0) return true;
    else return false;
}

//function findInetrsection1(aPts) {
//    /* проверка на пересечение области */
//    let bLat = aPts[0][0];
//    let bLng = aPts[0][1];
//    let lat = bLat;
//    let lng = bLng;
//    let lines = [];
//    for (let m = 1; m < aPts.length; m++) {
//        let line = new ymaps.Polyline([[lat, lng], [aPts[m][0], aPts[m][1]]]);
//        map.geoObjects.add(line);
//        lat = aPts[m][0];
//        lng = aPts[m][1];
//        lines.push(line);
//    }
//    lines.push(new ymaps.Polyline([[lat, lng], [bLat, bLng]]));
//    lines.forEach(function (el) {
//        map.geoObjects.add(el)
//    });
//    let hasIntersect = 0;
//    for (let m = 0; m < lines.length; m++) {
//        for (let n = 0; n < lines.length; n++) {
//            if (m != n) {
//                let coord1 = lines[m].geometry.getCoordinates();
//                let coord2 = lines[n].geometry.getCoordinates();
//                if (coord1[0].join() != coord2[0].join() &&
//                    coord1[0].join() != coord2[1].join() &&
//                    coord1[1].join() != coord2[0].join() &&
//                    coord1[1].join() != coord2[1].join()) {
//                    let obj = ymaps.geoQuery(lines[m]).searchIntersect(lines[n]);
//                    if (obj.getLength() > 0) hasIntersect += 1;
//                }
//            }
//            if (hasIntersect > 0) break;
//        }
//        if (hasIntersect > 0) break;
//    }
//    lines.forEach(function (el) {
//        map.geoObjects.remove(el)
//    });
//    if (hasIntersect > 0) return true;
//    else return false;
//}


function showAreaFilterWidget() {
    /* показ окна фильтра зон доставки */
    let wgt = document.getElementById("areasFilter");
    if (wgt.classList.contains("dragWidget--active")) {
        wgt.classList.remove("dragWidget--active");
        document.getElementById("btn_showFilterWidget").children[0].src = "/images/AreaEditor/filter_off.png";
    }
    else {
        wgt.classList.add("dragWidget--active");
        document.getElementById("btn_showFilterWidget").children[0].src = "/images/AreaEditor/filter_on.png";
    }
}

function showAreaWidget() {
    /* показ окна настройки зон доставки */
    let wgt = document.getElementById("areaProperties");
    if (wgt.classList.contains("dragWidget--active")) {
        wgt.classList.remove("dragWidget--active");
        document.getElementById("btn_showAreaWidget").children[0].src = "/images/AreaEditor/area_open.png";
    }
    else {
        wgt.classList.add("dragWidget--active");
        document.getElementById("btn_showAreaWidget").children[0].src = "/images/AreaEditor/area_close.png";
    }
}

function fillParentAreaSelect(lvl, prnt) {
    /* заполнение списка родительских зон для определённого уровня зоны */
    let selParentArea = document.getElementById("sel_areaParent");
    let item = document.createElement('option');
    while (selParentArea.children.length) selParentArea.removeChild(selParentArea.children[0]);
    item.value = "0";
    item.text = "";
    selParentArea.appendChild(item.cloneNode(true));
    arrayAreaList.forEach(function (el) {
        if (lvl == el.level) {
            item.value = el.id;
            item.text = el.name;
            selParentArea.appendChild(item.cloneNode(true));
        }
    });
    if (selParentArea.children.length > 0) selParentArea.value = prnt;
}

function getAreaIntervals(areaId) {
    try {
        //arrayAreaList = [];
        $.ajax({
            url: '/AreaEditor/GetAreaIntervals',
            data: { areaId: areaId},
            type: 'POST',
            success: function (data) {
                let json = JSON.parse(data).AreaIntervals;
                let innerHTML = '';
                let D = new Date();
                let today = dateFormat(D, 'yyyymmdd');
                D.setDate(D.getDate() - D.getDay() + (D.getDay() ? 7 : 0));
                let weekEnd = dateFormat(D, 'yyyymmdd');
                let firstScroll = '';
                if (json.length > 0) {
                    json.forEach(function (el) {
                        let startDay = dateFormat(el.Start, 'ddd');
                        let endDay = dateFormat(el.End, 'ddd');                        
                        let trAdd = (weekEnd < dateFormat(el.Start, 'yyyymmdd')) ? '' : 'class="intervalT"';
                        if (firstScroll == '' && today == dateFormat(el.Start, 'yyyymmdd')) firstScroll = ' id="scrollToThis"';
                        innerHTML += '<tr ' + firstScroll + trAdd + ' onclick="setSingleRowSelect(this)"><td>' + dateFormat(el.Start, 'dd.mm.yyyy HH:MM') +
                            ((startDay == 'Сб' || startDay == 'Вс') ? '</td><td style="color: red;">' : '</td><td>') + startDay +
                            '</td><td>' + dateFormat(el.End, 'dd.mm.yyyy HH:MM') +
                            ((endDay == 'Сб' || endDay == 'Вс') ? '</td><td style="color: red;">' : '</td><td>') + endDay +
                            '</td><td hidden>' + el.id + '</td></tr>';
                    });
                }
                document.getElementById("areaIntervalsBody").innerHTML = innerHTML;
                document.getElementById("scrollToThis").scrollIntoView({ block: "center", inline: "center" });
                document.getElementById("scrollToThis").click();
            },
            error: function () { alert('Не удалось получить список интервалов'); }
        });
    }
    catch (e) {
        alert('Ошибка ' + e.name + ":" + e.message + "\n" + e.stack);
    }
}



///* >> исправленные функции */
//function yandex_initialize() {
//    ymaps.ready(function () {
//        map = new ymaps.Map("map",
//            {
//                center: [gpMoscowCenter.lat, gpMoscowCenter.lng]
//                , zoom: 10
//                , behaviors: ["drag", "scrollZoom"]
//                , controls: []
//            },
//            {
//                autoFitToViewport: "none"//"always"
//                , minZoom: 3
//                , maxZoom: 19
//                , suppressMapOpenBlock: true
//            }
//        );
//        map.controls.add("typeSelector", { position: { right: 5, top: 5 } });
//        map.controls.add("zoomControl", { position: { left: 5, top: 40 } });
//        map.events.add(["click"], yandex_MapClick);
//    })
//}

//function yandex_MapClick(event) {
//    var coords = event.get('coords');
//    var str = coords[0] + "," + coords[1];
//    SetValueOf("ClickCoord", str);
//}

//function SetArrayOfPolygonNew(aPolygons) {
//    ClearPolygonList();
//    try {
//        //var nm ;
//        var obj;
//        var sid;
//        for (var ind = 0; ind < aPolygons.length; ind++) {
//            //nm ="\u0420\u0435\u0433\u0438\u043E\u043D №"+ind;
//            obj = aPolygons[ind];
//            var points = [];
//            for (var m = 0; m < obj.latlngarr.length; m++) {
//                var point = [];
//                point.push(obj.latlngarr[m].lat);
//                point.push(obj.latlngarr[m].lng);
//                points.push(point);
//            }
//            yandex_CreatePolygonNew(points, obj.rgbline, obj.rgbfill, obj.name, obj.name, obj.id, 0, obj.parentid, "", obj.level, obj.routenum, obj.sessionid);
//        }
//    }
//    catch (e) { SendMessage('ERROR|' + getFnName(arguments.callee) + ': ' + e.message) }
//    finally { }
//}

//function ClearPolygonList() {
//    for (var i = PolygonList.length - 1; i >= 0; i--) { map.geoObjects.remove(PolygonList[i]); }
//    PolygonList = [];
//}

//function ClearMarkerList() {
//    for (var i = MarkerList.length - 1; i >= 0; i--) { map.geoObjects.remove(MarkerList[i]); }
//    MarkerList = [];
//}

//function ClearRouteList() {
//    map.geoObjects.remove(route);
//    route = null;
//}

//function yandex_PolygonClick(event) {
//    yandex_MapClick(event);
//    yandex_PolygonInfo(event.get("target"));
//}

//function yandex_PolygonInfo(polygon) {
//    var Result = yandex_PolygonAbout(polygon);
//    SetValueOf("AreaXML", Result);
//}

//function yandex_PolygonAbout(polygon) {
//    var ArrPoints = polygon.geometry.getCoordinates()[0];
//    var Points = "";
//    if (ArrPoints[0] == ArrPoints[ArrPoints.length - 1]) ArrPoints.splice(ArrPoints.length - 1, 1);
//    ArrPoints.forEach(function (el) {
//        Points += '<LATLNG><LAT>' + el[0] + '</LAT><LNG>' + el[1] + '</LNG></LATLNG>\n';
//    });
//    let intersected = findInetrsection(ArrPoints);
//    if (intersected) document.getElementById("inp_areaName").style.backgroundColor = "lightsalmon";
//    else document.getElementById("inp_areaName").style.backgroundColor = "white";
//    document.getElementById("inp_areaName").value = polygon.metaDataProperty.Name;
//    document.getElementById("areaIntersectsIndicator").hidden = !intersected;
//    document.getElementById("sel_areaLevel").value = polygon.metaDataProperty.Level;
//    document.getElementById("inp_parentArea").value = polygon.metaDataProperty.ParentID;
//    fillParentAreaSelect(polygon.metaDataProperty.Level - 1, polygon.metaDataProperty.ParentID);
//    document.getElementById("inp_fillColor").value = "#" + polygon.options.get("fillColor").toUpperCase();
//    document.getElementById("inp_lineColor").value = "#" + polygon.options.get("strokeColor").toUpperCase();
//    getAreaIntervals(polygon.metaDataProperty.ID);
//    selectedAreaSessionId = polygon.metaDataProperty.SessionID;
//    var Result =
//        '<AREA>' + '\n' +
//        '<_MODE>YANDEX</_MODE>\n' +
//        '<_INDEX>' + polygon.metaDataProperty._index + '</_INDEX>\n' +
//        '<_POINTS>' + ArrPoints.length + '</_POINTS>\n' +
//        '<SESSIONID>' + polygon.metaDataProperty.SessionID + '</SESSIONID>\n' +
//        '<RECORDSTATE>' + polygon.metaDataProperty.RecordState + '</RECORDSTATE>\n' +
//        '<ID>' + polygon.metaDataProperty.ID + '</ID>' + '\n' +
//        '<PARENTID>' + polygon.metaDataProperty.ParentID + '</PARENTID>\n' +
//        '<NAME>' + polygon.metaDataProperty.Name + '</NAME>\n' +
//        '<LEVEL>' + polygon.metaDataProperty.Level + '</LEVEL>\n' +
//        '<RGBLINE>' + polygon.options.get("strokeColor").toUpperCase() + '</RGBLINE>\n' +
//        '<RGBFILL>' + polygon.options.get("fillColor").toUpperCase() + '</RGBFILL>\n' +
//        '<ROUTENUM>' + polygon.metaDataProperty.RouteNum + '</ROUTENUM>\n' +
//        Points +
//        '</AREA>';
//    return Result;
//}

//function SetBoundsForPoints(aPoints) {
//    try {
//        if (aPoints.length == 0) { return; }
//        var minLat = aPoints[0].lat;
//        var maxLat = aPoints[0].lat;
//        var minLon = aPoints[0].lng;
//        var maxLon = aPoints[0].lng;
//        var lat = 0;
//        var lng = 0;
//        for (var i = 0; i < aPoints.length; i++) {
//            lat = aPoints[i].lat;
//            lng = aPoints[i].lng;
//            if (lat > maxLat) { maxLat = lat };
//            if (lat < minLat) { minLat = lat };
//            if (lng > maxLon) { maxLon = lng };
//            if (lng < minLon) { minLon = lng };
//        }
//        map.setBounds([[minLat, minLon], [maxLat, maxLon]]);
//    }
//    catch (e) { SendMessage('ERROR|' + getFnName(arguments.callee) + ': ' + e.message); }
//    finally { }
//}

//function yandex_SetCenter(aLat, aLng) {
//    map.setCenter([aLat, aLng]);
//};

//function SendToBack(aSessionID) {
//    PolygonList.forEach(function (el) {
//        if (el.metaDataProperty.SessionID == aSessionID || el.metaDataProperty.ID == aSessionID)
//            el.options.set("zIndex", el.options.get("zIndex") - 1);
//    });
//}

//function yandex_PolygonStartEditing(aIndex) {
//    polygon = PolygonList[aIndex];
//    if (polygon && !polygon.editor.state.get("editing")) { polygon.editor.startEditing(); }
//    polygon.metaDataProperty.RecordState = 1;
//    yandex_PolygonInfo(polygon);
//}

//function yandex_PolygonStopEditing(aIndex, aAreaID) {
//    polygon = PolygonList[aIndex];
//    if (polygon && polygon.editor.state.get("editing")) { polygon.editor.stopEditing(); }
//    polygon.metaDataProperty.ID = aAreaID;
//    polygon.metaDataProperty.RecordState = 0
//    yandex_PolygonInfo(polygon);
//}

//function CreateNewArea(lat, lng, num, dist, clrBord, clrFill, Descr, SessionID) {
//    if (!CanAreaEdit) { return };
//    yandex2_CreateNewArea(lat, lng, num, dist, clrBord, clrFill, Descr, SessionID);
//}

//function ChangeProps_BySessionID(aSessionID, aProp, aValue) {
//    var ind = -1;
//    for (var i = 0; i < PolygonList.length; i++) {
//        if (PolygonList[i].metaDataProperty.SessionID == aSessionID) {
//            ind = i;
//            break;
//        }
//    }
//    if (ind != -1) { yandex_ChangeProps_Value(ind, aProp, aValue); }
//}

//function yandex_ChangeProps_Value(aIndex, aProp, aValue) {
//    var polygon = PolygonList[aIndex];
//    switch (aProp) {
//        case ipID:
//            polygon.metaDataProperty.ID = aValue;
//            break;
//        case ipParentID:
//            polygon.metaDataProperty.ParentID = aValue;
//            break;
//        case ipName:
//            polygon.metaDataProperty.Name = aValue;
//            polygon.name = aValue;
//            polygon.description = aValue;
//            polygon.properties.set("hintContent", aValue);
//            break;
//        case ipLevel:
//            polygon.metaDataProperty.Level = aValue;
//            break;
//        case ipLineColor:
//            polygon.metaDataProperty.LineColor = aValue;
//            yandex_ChangeProps_Colors(aIndex, polygon.metaDataProperty.LineColor, polygon.metaDataProperty.FillColor);
//            break;
//        case ipFillColor:
//            polygon.metaDataProperty.FillColor = aValue;
//            yandex_ChangeProps_Colors(aIndex, polygon.metaDataProperty.LineColor, polygon.metaDataProperty.FillColor);
//            break;
//        case ipRoute:
//            polygon.metaDataProperty.RouteNum = aValue;
//            break;
//        case ipSessionID:
//            polygon.metaDataProperty.SessionID = aValue; // -- !! в крайнем случае
//            break;
//        default:
//            break;
//    }
//    yandex_PolygonInfo(polygon);
//}

//function yandex_ChangeProps_Colors(aIndex, lineColor, fillColor) {
//    var polygon = PolygonList[aIndex];
//    polygon.metaDataProperty.LineColor = lineColor;
//    polygon.metaDataProperty.FillColor = fillColor;
//    polygon.options.set("strokeColor", polygon.metaDataProperty.LineColor);
//    polygon.options.set("fillColor", polygon.metaDataProperty.FillColor);
//}

//function yandex2_CreateNewArea(lat, lng, num, dist, clrBord, clrFill, Descr, SessionID) {
//    var ind = -1;
//    var pts = [];
//    var minLat = lat;
//    var maxLat = lat;
//    var minLon = lng;
//    var maxLon = lng;
//    var llat = 0;
//    var llng = 0;
//    for (i = 0; i < num; i++) {
//        angle = (360 / num) * i * Math.PI / 180;
//        delta = [Math.cos(angle), Math.sin(angle)];
//        pt = ymaps.coordSystem.geo.solveDirectProblem([lat, lng], delta, dist).endPoint;
//        llat = pt[0];
//        llng = pt[1];
//        if (llat > maxLat) { maxLat = llat };
//        if (llat < minLat) { minLat = llat };
//        if (llng > maxLon) { maxLon = llng };
//        if (llng < minLon) { minLon = llng };
//        pts.push(pt);
//    }
//    ind = yandex_CreatePolygonNew(pts, clrBord, clrFill, Descr, Descr, "0", "1", "0", Descr, "3", "0", "1111111", SessionID);
//    if (ind > -1) {
//        var xml = yandex_PolygonAbout(PolygonList[ind]);
//        SetValueOf("AreaXML", xml);
//        map.setBounds([[minLat, minLon], [maxLat, maxLon]]);
//        PolygonList[ind].editor.startEditing();
//    }
//}

//function yandex_CreatePolygonNew(aPts, aBorderColor, aFillColor, aShortDescr, aFullDescr, aID, aRecordState, aParentID, aName, aLevel, aRouteNum, aSessionID) {
//    try {
//        var res = -1;
//        if (aPts.length == 0) return res
//        var polygon = new ymaps.Polygon([aPts],
//            { hintContent: aShortDescr },
//            {
//                fill: true,
//                stroke: true,
//                strokeWidth: 1,
//                strokeColor: aBorderColor,
//                fillColor: aFillColor,
//                zIndex: 100,
//                opacity: 0.33
//            });

//        var ind = PolygonList.length;
//        var md =
//        {
//            _index: ind ? ind : 0
//            , ID: aID ? aID : 0
//            , RecordState: aRecordState ? aRecordState : 0
//            , ParentID: aParentID ? aParentID : 0
//            , Name: aName ? aName : aFullDescr
//            , Level: aLevel ? aLevel : 3
//            , RouteNum: aRouteNum ? aRouteNum : aID ? aID : 0
//            , LineColor: aBorderColor ? aBorderColor : "FF0000"
//            , FillColor: aFillColor ? aFillColor : "FFFF00"
//            , SessionID: aSessionID ? aSessionID : "index_" + PolygonList.length
//        };
//        polygon.id = md.ID;
//        polygon.name = aShortDescr;
//        polygon.description = aFullDescr;
//        polygon.metaDataProperty = md;

//        if (map && polygon) {
//            map.geoObjects.add(polygon);
//            polygon.events.add("click", yandex_PolygonClick);
//        }
//        if (polygon) {
//            PolygonList.push(polygon);
//            polygon.metaDataProperty._index = PolygonList.length - 1;
//            res = polygon.metaDataProperty._index;
//        }
//    }
//    catch (e) { SendMessage('ERROR|' + getFnName(arguments.callee) + ': ' + e.message) }
//    finally { return res; }
//}

///* << исправленные функции */

////https://msdn.microsoft.com/en-us/library/ms536951%28v=vs.85%29.aspx
////https://msdn.microsoft.com/en-us/library/aa703876%28v=vs.85%29.aspx

//function yandex_GetGeoPointList(aGMPointArray, GeoPoints) {
//    var points = [];
//    var LatLon = [];
//    points = aGMPointArray.split(';');
//    for (var pt = 0; pt < points.length; pt++) {
//        LatLon = points[pt].split(',');
//        GeoPoints.push(new YMaps.GeoPoint(LatLon[1], LatLon[0]));
//    }
//}

//function yandex_GetPolygonStyle(aID, aBorderColor, aFillColor) {
//    var StyleName = "polygon#" + aID;
//    var style = new YMaps.Style("default#greenSmallPoint");
//    style.polygonStyle = new YMaps.PolygonStyle();
//    style.polygonStyle.fill = 1;
//    style.polygonStyle.outline = 1;
//    style.polygonStyle.strokeWidth = 1;
//    style.polygonStyle.strokeColor = aBorderColor + "FF";
//    style.polygonStyle.fillColor = aFillColor + "40";
//    YMaps.Styles.add(StyleName, style);
//    return StyleName;
//}

//// ----
//function yandex_CreatePolygon(aPath, aBorderColor, aFillColor, aShortDescr, aFullDescr, aID, aRecordState, aParentID, aName, aLevel, aRouteNum, aSessionID) {
//    var res = -1;
//    var GeoPoints = [];
//    var area_DblClick = null;
//    var ind = PolygonList.length;
//    var id = aID;
//    var md = {
//        _index: ind ? ind : 0
//        , ID: aID ? aID : 0
//        , RecordState: aRecordState ? aRecordState : 0
//        , ParentID: aParentID ? aParentID : 0
//        , Name: aName ? aName : aFullDescr
//        , Level: aLevel ? aLevel : 3
//        , RouteNum: aRouteNum ? aRouteNum : aID ? aID : 0
//        , LineColor: aBorderColor ? aBorderColor : "FF0000"
//        , FillColor: aFillColor ? aFillColor : "FFFF00"
//        , SessionID: aSessionID ? aSessionID : "index_" + PolygonList.length
//    };
//    var interact;
//    interact = YMaps.Interactivity.INTERACTIVE;
//    yandex_GetGeoPointList(aPath, GeoPoints);
//    if (GeoPoints.length != 0) {
//        var polygon = new YMaps.Polygon(GeoPoints,
//            { style: yandex_GetPolygonStyle(id, aBorderColor, aFillColor), hasHint: 1, hasBalloon: 0, interactive: interact, zIndex: 0 });
//        polygon.id = md.ID;
//        polygon.name = aShortDescr;
//        polygon.description = aFullDescr;
//        polygon.metaDataProperty = md;

//        YMaps.Events.observe(polygon, polygon.Events.Click, yandex_PolygonClick);
//        YMaps.Events.observe(polygon, polygon.Events.PositionChange, yandex_PolygonInfo);
//        if ((map) && (polygon)) { map.addOverlay(polygon) };
//    }
//    if (polygon) {
//        PolygonList.push(polygon);
//        polygon.metaDataProperty._index = PolygonList.length - 1;
//        res = polygon.metaDataProperty._index;
//    }
//    return res;
//}

//function yandex_CreateMarker(aLatitude, aLongitude, aDescription, aCorner) {
//    var aCurStyle = GreenMarkerIcon;
//    if (aDescription.lastIndexOf(")*") != -1) { aCurStyle = RedMarkerIcon; }
//    if (aCorner) { aCurStyle = BlueMarkerIcon; }

//    var marker = new YMaps.Placemark(new YMaps.GeoPoint(aLongitude, aLatitude),
//        {
//            draggable: 0,
//            hideIcon: false,
//            interactive: YMaps.Interactivity.INTERACTIVE,
//            hintOptions: { maxWidth: 250, showTimeout: 200, offset: new YMaps.Point(5, 5) },
//            balloonOptions: { maxWidth: 250, hasCloseButton: true, mapAutoPan: 1 }
//        });
//    marker.description = aDescription;
//    var MarkerStyle = new YMaps.Style();
//    MarkerStyle.iconStyle = new YMaps.IconStyle();
//    MarkerStyle.iconStyle.size = new YMaps.Point(10, 10); // -data-
//    MarkerStyle.iconStyle.offset = new YMaps.Point(-5, -5);
//    MarkerStyle.iconStyle.href = aCurStyle;
//    marker.setStyle(MarkerStyle);
//    map.addOverlay(marker);
//    MarkerList.push(marker);
//}

//function yandex_RoutePack(aObj, aIndex, aWPArray) {
//    var pt = new YMaps.GeoPoint(aObj.lng, aObj.lat);
//    var wp = null;
//    for (var i = 0; i < aWPArray.length; i++) {
//        if (aWPArray[i].getGeoPoint().toString() == pt.toString()) {
//            aWPArray[i].metaDataProperty.Caption = aWPArray[i].metaDataProperty.Caption + "," + (aIndex + 1);
//            aWPArray[i].description = aWPArray[i].description +/*'<tr><td>'+*/"<div class='fi80red'>\u0422\u043E\u0447\u043A\u0430\u0020\u2116" + (aIndex + 1) + "</div>" + aObj.descr/*+'</td></tr>'*/;
//            return;
//        }
//    }
//    wp = new YMaps.WayPoint(pt);
//    wp.metaDataProperty.Caption = "" + (aIndex + 1);
//    wp.description = "<div class='fi80red'>\u0422\u043E\u0447\u043A\u0430\u0020\u2116" + (aIndex + 1) + "</div>" + aObj.descr;
//    aWPArray.push(wp);
//}

//function yandex_Route(aPoints) {
//    try {
//        if (route) { map.removeOverlay(route); route = null };
//        rs = new YMaps.Style();
//        rs.lineStyle = new YMaps.LineStyle();
//        rs.lineStyle.strokeColor = "000000";
//        rs.lineStyle.strokeWidth = 2;
//        rs.hideIcon = false;
//        YMaps.Styles.add("router#RouteLine", rs);
//        var points = []
//        var waypoints = []
//        var params = [];
//        var wp = null;
//        for (var i = 0; i < aPoints.length; i++) { yandex_RoutePack(aPoints[i], i, waypoints); }
//        for (var i = 0; i < waypoints.length; i++) { points.push(waypoints[i].getGeoPoint()) }
//        route = new YMaps.Router(points, [], { viewAutoApply: true });
//        route.setStyle(rs);
//        route.metaDataProperty.Source = aPoints;
//        route.metaDataProperty.WayPoints = waypoints;
//        YMaps.Events.observe(route, route.Events.Load, function () {
//            for (var pt = 0; pt < points.length; pt++) {
//                wp = route.getWayPoint(pt);
//                wp.setStyle("default#nightSmallPoint");
//                wp.setIconContent(waypoints[pt].metaDataProperty.Caption);
//                wp.setOptions({ hideIcon: false });
//                //var descr='<table>'+waypoints[pt].description+'</table>';
//                wp.setBalloonContent(waypoints[pt].description);
//                wp.setBalloonOptions({ maxWidth: 200, hasCloseButton: true, mapAutoPan: 1 });
//            }
//        });
//        map.addOverlay(route);
//    } catch (e) { SendMessage('ERROR|' + getFnName(arguments.callee) + ': ' + e.message); }
//}




//function yandex_DeleteArea(aIndex) {
//    var polygon = PolygonList[aIndex];
//    var del = PolygonList.splice(aIndex, 1);
//    for (var i = 0; i < del.length; i++) {
//        del[i].metaDataProperty.ID = Math.abs(del[i].metaDataProperty.ID) * -1;
//        DeletedPolygonList.push(del[i]);
//    };
//    map.removeOverlay(polygon);
//    var Result = yandex_PolygonAbout(DeletedPolygonList[DeletedPolygonList.length - 1]);
//    SetValueOf("AreaXML", Result);
//}

//// ----------------------------------------------------------------------------------


//function SetArrayOfPolygon(aPolygons) {
//    ClearPolygonList();
//    var StrPolygons = aPolygons.split(AreaDelimiter);
//    var Params = []
//    var ind = 0;
//    var nm = "";
//    var desc = "";
//    for (var i = 0; i < StrPolygons.length; i++) {
//        Params = StrPolygons[i].split(GroupDelimiter);
//        ind = i + 1;
//        nm = "\u0420\u0435\u0433\u0438\u043E\u043D №" + ind;
//        if (Params[3]) nm = Params[3];
//        desc = nm;
//        if (Params[4]) desc = Params[4];
//        yandex_CreatePolygon(Params[0], Params[1].substring(1, 10), Params[2].substring(1, 10), nm, desc, Params[5], Params[6], Params[7], Params[8], Params[9], Params[10], Params[11]);
//    }
//}

//function SetArrayOfMarker(aPoints) {
//    ClearMarkerList();
//    for (var i = 0; i < aPoints.length; i++) {
//        yandex_CreateMarker(aPoints[i].lat, aPoints[i].lng, aPoints[i].descr);
//    }
//    SetBoundsForPoints(aPoints);
//}

//function CreateRoute(aMarkers) {
//    yandex_Route(aMarkers);
//    SetBoundsForPoints(aMarkers);
//}

//function ChangeProps_Value(aIndex, aProp, aValue) {
//    yandex_ChangeProps_Value(aIndex, aProp, aValue)
//}


//function DeleteAreaByIndex(aIndex) {
//    yandex_DeleteArea(aIndex)
//}

//function DeleteAreaBySessionID(aSessionID) {
//    var ind = -1;
//    for (var i = 0; i < PolygonList.length; i++) {
//        if (PolygonList[i].metaDataProperty.SessionID == aSessionID) {
//            ind = i;
//            break;
//        }
//    }
//    if (ind != -1) { DeleteAreaByIndex(ind); }
//}

//function EditAreaBySessionID(aSessionID) {
//    var ind = -1;
//    for (var i = 0; i < PolygonList.length; i++) {
//        if (PolygonList[i].metaDataProperty.SessionID == aSessionID) {
//            ind = i;
//            break;
//        }
//    }
//    if (ind != -1) { yandex_PolygonStartEditing(ind); }
//}

//function SaveAreaBySessionID(aSessionID, AreaID) {
//    var ind = -1;
//    for (var i = 0; i < PolygonList.length; i++) {
//        if (PolygonList[i].metaDataProperty.SessionID == aSessionID) {
//            ind = i;
//            break;
//        }
//    }
//    if (ind != -1) { yandex_PolygonStopEditing(ind, AreaID); }
//}

//function PolygonAboutBySessionID(aSessionID) {
//    var ind = -1;
//    for (var i = 0; i < PolygonList.length; i++) {
//        if (PolygonList[i].metaDataProperty.SessionID == aSessionID) {
//            yandex_PolygonAbout(PolygonList[i]);
//            return;
//        }
//    }
//}


//// --- common utils functions ---------------------------------------------------------------------
//function GetObjectProps(aObj, Level) {
//    var Result = ""; for (var key in aObj) { Result = Result + key + ":" + aObj[key] + '\n' } return Result;
//}
//function SetValueOf(aName, aValue) {
//    if (typeof this[aName] != "undefined") { this[aName].value = aValue; if (this[aName].id != "forDebug") SendMessage(aName); } else { }
//}
//function SendMessage(aText) {
//    try { window.navigate("#" + aText); } catch (e) { var url = "#" + aText; location.href = url; }/*finally{alert(aText);}*/
//}
//function getFnName(fn) {
//    return fn.toString().match(/function ([^(]*)\(/)[1];
//} //http://javascript.ru/forum/misc/4895-uznat-imya-funkcii.html
//        // ------------------------------------------------------------------------
