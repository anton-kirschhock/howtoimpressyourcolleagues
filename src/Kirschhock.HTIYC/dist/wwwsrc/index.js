"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.IndexPageModel = void 0;
require("core-js/modules/es.promise");
var IndexPageModel = /** @class */ (function () {
    function IndexPageModel(isFirst) {
        var _this = this;
        if (isFirst === void 0) { isFirst = true; }
        this.isFirst = isFirst;
        this.baseUrl = window.location.origin + "/api/facts";
        this.NextFact.onclick = function (event) { return _this.OnNextFact(event); };
    }
    Object.defineProperty(IndexPageModel.prototype, "BannerTitle", {
        get: function () {
            return document.querySelector("#banner-title");
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(IndexPageModel.prototype, "BoxContainer", {
        get: function () {
            return document.querySelector("div.box-container");
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(IndexPageModel.prototype, "RenderArea", {
        get: function () {
            return document.querySelector("div#renderArea");
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(IndexPageModel.prototype, "NextFact", {
        get: function () {
            return document.querySelector("button#btnNextFact");
        },
        enumerable: false,
        configurable: true
    });
    IndexPageModel.prototype.OnNextFact = function (event) {
        return __awaiter(this, void 0, void 0, function () {
            var resp, jsonBody, err_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        event.preventDefault();
                        this.isFirst = false;
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 4, , 5]);
                        return [4 /*yield*/, fetch(this.baseUrl + "/random")];
                    case 2:
                        resp = _a.sent();
                        return [4 /*yield*/, resp.json()];
                    case 3:
                        jsonBody = (_a.sent());
                        console.log(jsonBody);
                        this.BoxContainer.classList.remove("before-first-time-click");
                        this.BannerTitle.innerText = "Impress them with this:";
                        this.NextFact.innerText = "Let's Continue inpressing!";
                        this.renderFact(jsonBody);
                        return [3 /*break*/, 5];
                    case 4:
                        err_1 = _a.sent();
                        console.error("Something went wrong while fetching the latest Fact...", err_1);
                        return [3 /*break*/, 5];
                    case 5: return [2 /*return*/];
                }
            });
        });
    };
    IndexPageModel.prototype.renderFact = function (fact) {
        while (this.RenderArea.firstChild != null) {
            this.RenderArea.removeChild(this.RenderArea.firstChild);
        }
        var p = document.createElement("p");
        p.innerText = fact.title;
        p.classList.add("title");
        var ul = document.createElement("ul");
        if (fact.description !== undefined && fact.description != null) {
            var descr = document.createElement("li");
            descr.innerText = "" + fact.description;
            descr.classList.add("description");
            ul.appendChild(descr);
        }
        if (fact.readMoreLink) {
            var readMore = document.createElement("a");
            readMore.href = fact.readMoreLink;
            readMore.classList.add("read-more");
            readMore.text = "Read more";
            ul.appendChild(readMore);
        }
        this.RenderArea.appendChild(p);
        this.RenderArea.appendChild(ul);
    };
    return IndexPageModel;
}());
exports.IndexPageModel = IndexPageModel;
var pageModel = new IndexPageModel();
//# sourceMappingURL=index.js.map