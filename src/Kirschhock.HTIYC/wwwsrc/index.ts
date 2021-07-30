import "core-js/modules/es.promise";
export interface Topic {
  id: string;
  name: string;
  displayName: string;
  description: string;
}

export interface Fact {
  id: string;
  name: string;
  title: string;
  description: string;
  readMoreLink: string;
  topic: Topic;
}

export class IndexPageModel {
  private readonly baseUrl = `${window.location.origin}/api/facts`;

  private get RenderArea(): HTMLDivElement {
    return document.querySelector("div#renderArea") as HTMLDivElement;
  }
  private get NextFact(): HTMLButtonElement {
    return document.querySelector("button#btnNextFact") as HTMLButtonElement;
  }

  constructor() {
    this.NextFact.onclick = (event) => this.OnNextFact(event);
  }

  private async OnNextFact(event: Event) {
    event.preventDefault();

    try {
      const resp = await fetch(`${this.baseUrl}/random`);
      const jsonBody = (await resp.json()) as Fact;
      console.log(jsonBody);
      this.renderFact(jsonBody);
    } catch (err) {
      console.error(
        `Something went wrong while fetching the latest Fact...`,
        err
      );
    }
  }

  private renderFact(fact: Fact): void {
    while (this.RenderArea.firstChild != null) {
      this.RenderArea.removeChild(this.RenderArea.firstChild);
    }

    const p = document.createElement("p") as HTMLParagraphElement;
    p.innerText = fact.title;
    const ul = document.createElement("ul") as HTMLUListElement;

    const id = document.createElement("li") as HTMLLIElement;
    id.innerText = `#${fact.id}`;
    ul.appendChild(id);

    const descr = document.createElement("li") as HTMLLIElement;
    descr.innerText = `Description: ${fact.description}`;
    ul.appendChild(descr);

    const timeStamp = document.createElement("li") as HTMLLIElement;
    timeStamp.innerText = `${new Date()}`;
    ul.appendChild(timeStamp);

    this.RenderArea.appendChild(p);
    this.RenderArea.appendChild(ul);
  }
}

const pageModel = new IndexPageModel();
