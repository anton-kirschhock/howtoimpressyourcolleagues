"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.IndexPageModel = void 0;

require("core-js/modules/es.promise.js");

require("core-js/modules/es.promise");

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

class IndexPageModel {
  get NextFact() {
    return document.querySelector("button#btnNextFact");
  }

  constructor() {
    _defineProperty(this, "baseUrl", "".concat(window.location.origin, "/api/facts"));

    this.NextFact.onclick = event => this.OnNextFact(event);
  }

  async OnNextFact(event) {
    event.preventDefault();

    try {
      const resp = await fetch("".concat(this.baseUrl, "/random"));
      const jsonBody = await resp.json();
      console.log(jsonBody);
    } catch (err) {
      console.error("Something went wrong while fetching the latest Fact...", err);
    }
  }

}

exports.IndexPageModel = IndexPageModel;
const pageModel = new IndexPageModel();
