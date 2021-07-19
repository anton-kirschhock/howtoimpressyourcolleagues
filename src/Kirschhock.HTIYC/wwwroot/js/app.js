"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.IndexPageModel = void 0;

class IndexPageModel {
  get NextFact() {
    return document.querySelector("button#btnNextFact");
  }

  constructor() {
    this.NextFact.onclick = event => this.OnNextFact(event);
  }

  OnNextFact(event) {
    event.preventDefault();
    console.log("Next Fact button pressed");
  }

}

exports.IndexPageModel = IndexPageModel;
const pageModel = new IndexPageModel();
