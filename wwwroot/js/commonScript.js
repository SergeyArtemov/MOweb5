function setSingleRowSelect(obj)
/* установка выделения при щелчке на строке таблицы. Таблица должна содержать класс "table-select"*/
{
    var self = $(obj);
    var table = self.closest('table');
    var activeRow = table.data('active-row');
    if (activeRow) {
        activeRow.removeClass('active');
    }
    activeRow = self.closest('tr');
    table.data('active-row', activeRow);
    activeRow.addClass('active');
};

function setSingleCellSelect(ths, thsparent)
/* установка выделения ячейки таблицы при щелчке в ней */ {
    var allcells = thsparent.getElementsByTagName('td');
    for (var a = 0, lencells = allcells.length; a < lencells; a++) {
        allcells[a].classList.remove('selected-ship-cell');
    }
    ths.classList.add('selected-ship-cell');
};


/* >> ФУНКЦИИ ФОРМАТИРОВАНИЯ ДАТЫ*/
var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
        timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
        timezoneClip = /[^-+\dA-Z]/g,
        pad = function (val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len) val = "0" + val;
            return val;
        };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
            d = date[_ + "Date"](),
            D = date[_ + "Day"](),
            m = date[_ + "Month"](),
            y = date[_ + "FullYear"](),
            H = date[_ + "Hours"](),
            M = date[_ + "Minutes"](),
            s = date[_ + "Seconds"](),
            L = date[_ + "Milliseconds"](),
            o = utc ? 0 : date.getTimezoneOffset(),
            flags = {
                d: d,
                dd: pad(d),
                ddd: dF.i18n.dayNames[D],
                dddd: dF.i18n.dayNames[D + 7],
                m: m + 1,
                mm: pad(m + 1),
                mmm: dF.i18n.monthNames[m],
                mmmm: dF.i18n.monthNames[m + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                M: M,
                MM: pad(M),
                s: s,
                ss: pad(s),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
            };

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
        "Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб",
        "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"
    ],
    monthNames: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};
/* << ФУНКЦИИ ФОРМАТИРОВАНИЯ ДАТЫ*/

/**Parses string formatted as YYYY-MM-DD to a Date object.
 * If the supplied string does not match the format, an 
 * invalid Date (value NaN) is returned.
 * @@param {string} dateStringInRange format YYYY-MM-DD, with year in
 * range of 0000-9999, inclusive.
 * @@return {Date} Date object representing the string.
 */

function parseDateFromString(dateStringInRange) {
    var dateStr = dateStringInRange;
    var a = dateStr.split(" ");
    var d = a[0].split("-");
    var t = a[1].split(":");
    var date = new Date(d[0], (d[1] - 1), d[2], t[0], t[1], t[2]);
    return date;
}

/* >> CONTEXT MENU */
function addEventMenu(elementId, contentMenuId, evnt, parentElementId = null) {
    var elem = document.getElementById(elementId);
    var menu = document.getElementById(contentMenuId);
    var eventMenuActive = "event-menu--active";

    function initContextMenu() {
        elemEventListener();
        windowEventListener();
        menuListener();
    }

    function elemEventListener() {
        elem.addEventListener(evnt, function (e) {
            if (eventInsideElement(e, elementId) ||
                (parentElementId != null && e.currentTarget.id == parentElementId)) {
                e.preventDefault();
                closeAllMenu();
                toggleMenuOn();
                positionMenu(e);
            }
            else closeAllMenu();
        });
    }


    function menuListener() {
        window.addEventListener("click", function (e) {
            let parentElem = document.getElementById(parentElementId);
            if (menu.classList.contains("event-menu--active")) {
                if (!(e.srcElement.id == elementId || elem.contains(e.srcElement) ||
                    (parentElem != null && parentElem.contains(e.srcElement)) ||
                    menu.contains(e.srcElement) || e.srcElement.classList.contains("menuHead")))
                    menu.classList.remove("event-menu--active");
            }                
        });

        menu.addEventListener("click", function (e) {
            //if (!e.srcElement.classList.contains("menuHead"))
            if (e.srcElement.classList.contains("event-menu__item") ||
                e.srcElement.classList.contains("menuClose"))
                //closeAllMenu();
                menu.classList.remove("event-menu--active");
            else closeAllSubMenu(e.srcElement.parentNode);
        });

        menu.addEventListener("contextmenu", function (e) {
            e.preventDefault();
        });

        menu.addEventListener("mouseover", function (e) {
            var src = e.srcElement;
            if (!src.classList.contains("event-menu")) {
                while (!src.classList.contains("event-menu")) {
                    src = src.parentNode;
                }
            }
            if (e.srcElement.classList.contains("event-menu__item")) {
                closeAllSubMenu(e.srcElement.parentNode);
                for (i = 0; i < e.srcElement.children.length; i++) {
                    if (e.srcElement.children[i].classList.contains("subMenu")) {
                        var sm = e.srcElement.children[i];
                        sm.classList.add("subMenu--active");
                        sm.style.top = (e.srcElement.offsetTop) + "px";
                        sm.children[0].style.maxHeight = (Math.floor(window.innerHeight * 0.9)) + "px";
                        if (sm.offsetHeight > (window.innerHeight - e.y)) 
                            sm.style.top = window.innerHeight - e.y - sm.offsetHeight + e.srcElement.offsetTop + "px";
                        break;
                    }
                }
            }
            if (e.srcElement.tagName == 'LI' && !e.srcElement.children[0].children[0].children.length > 0) {
                e.srcElement.children[0].style.display = "none";
            }
        });
    }

    function closeAllSubMenu(menu) {
        var allSMenu = menu.getElementsByClassName("subMenu--active");
        for (i = 0; i < allSMenu.length; i++) {
            allSMenu[i].classList.remove("subMenu--active");
        }
    }

    function windowEventListener() {
        var arrows = [37, 38, 39, 40];
        window.onkeyup = function (e) {
            if (arrows.indexOf(e.keyCode) == -1) closeAllMenu();
        }
        window.onresize = function (e) {
            closeAllMenu();
        };
    }

    function toggleMenuOn() {
        menu.classList.add(eventMenuActive);
    }

    function positionMenu(e) {
        clickCoords = getPosition(e);
        clickCoordsX = clickCoords.x;
        clickCoordsY = clickCoords.y;
        menuWidth = menu.offsetWidth + 4;
        menuHeight = menu.offsetHeight + 4;
        windowWidth = window.innerWidth;
        windowHeight = window.innerHeight;
        if (menu.scrollHeight > windowHeight) {
            menu.style.maxHeight = windowHeight;
        }
        if ((windowWidth - clickCoordsX) < menuWidth) {
            menu.style.left = windowWidth - menuWidth + "px";
        } else {
            menu.style.left = clickCoordsX + "px";
        }
        if ((windowHeight - clickCoordsY) < menuHeight) {
            menu.style.top = windowHeight - menuHeight + "px";
        } else {
            menu.style.top = clickCoordsY + "px";
        }
    }

    function eventInsideElement(e, elementId) {
        var el = e.srcElement || e.target;
        if ((el.id == elementId || elem.contains(el)) && !el.classList.contains("orderButtonPushed")) return true;
        else return false;
    }
    initContextMenu();
};

function closeAllMenu() {
    var allMenu = document.getElementsByClassName("event-menu--active");
    for (i = 0; i < allMenu.length; i++) {
        allMenu[i].classList.remove("event-menu--active");
    }
}

function getPosition(e) {
    var posx = 0;
    var posy = 0;
    if (!e) var e = window.event;
    if (e.pageX || e.pageY) {
        posx = e.pageX;
        posy = e.pageY;
    } else if (e.clientX || e.clientY) {
        posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }
    return {
        x: posx,
        y: posy
    }
}
/* << CONTEXT MENU */



