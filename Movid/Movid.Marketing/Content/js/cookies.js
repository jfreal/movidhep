﻿/*! Cookies.js - 0.2.1; Copyright (c) 2012, Scott Hamper; http://www.opensource.org/licenses/MIT */
(function (f, e) {
    var b = function (c, d, a) { return 1 === arguments.length ? b.get(c) : b.set(c, d, a) }; b.get = function (c) { f.cookie !== b._cacheString && b._populateCache(); return b._cache[c] }; b.defaults = { path: "/" }; b.set = function (c, d, a) {
        a = { path: a && a.path || b.defaults.path, domain: a && a.domain || b.defaults.domain, expires: a && a.expires || b.defaults.expires, secure: a && a.secure !== e ? a.secure : b.defaults.secure }; d === e && (a.expires = -1); switch (typeof a.expires) {
            case "number": a.expires = new Date((new Date).getTime() + 1E3 * a.expires); break;
            case "string": a.expires = new Date(a.expires)
        } c = encodeURIComponent(c) + "=" + (d + "").replace(/[^!#-+\--:<-\[\]-~]/g, encodeURIComponent); c += a.path ? ";path=" + a.path : ""; c += a.domain ? ";domain=" + a.domain : ""; c += a.expires ? ";expires=" + a.expires.toGMTString() : ""; c += a.secure ? ";secure" : ""; f.cookie = c; return b
    }; b.expire = function (c, d) { return b.set(c, e, d) }; b._populateCache = function () {
        b._cache = {}; b._cacheString = f.cookie; for (var c = b._cacheString.split("; "), d = 0; d < c.length; d++) {
            var a = c[d].indexOf("="), g = decodeURIComponent(c[d].substr(0,
a)), a = decodeURIComponent(c[d].substr(a + 1)); b._cache[g] === e && (b._cache[g] = a)
        } 
    }; b.enabled = function () { var c = "1" === b.set("cookies.js", "1").get("cookies.js"); b.expire("cookies.js"); return c } (); "function" === typeof define && define.amd ? define(function () { return b }) : "undefined" !== typeof exports ? ("undefined" != typeof module && module.exports && (exports = module.exports = b), exports.Cookies = b) : window.Cookies = b
})(document);