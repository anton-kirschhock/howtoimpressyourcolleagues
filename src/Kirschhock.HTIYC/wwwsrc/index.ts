export class IndexPageModel{
    private get NextFact():HTMLButtonElement{
        return document.querySelector("button#btnNextFact") as HTMLButtonElement;
    }

    constructor() {
        this.NextFact.onclick = (event)=> this.OnNextFact(event);
    }

    private OnNextFact(event:Event){
        event.preventDefault();

        console.log("Next Fact button pressed");
    }
}


const pageModel = new IndexPageModel();
