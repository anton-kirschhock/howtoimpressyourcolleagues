import "core-js/modules/es.promise";

export interface Fact {
  id: string;
  name: string;
  title: string;
  description: string;
  readMoreLink: string;
}

export class IndexPageModel {
  private readonly baseUrl = `${window.location.origin}/api/facts`;

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
      const jsonBody = await resp.json();
      console.log(jsonBody);
    } catch (err) {
      console.error(
        `Something went wrong while fetching the latest Fact...`,
        err
      );
    }
  }
}

const pageModel = new IndexPageModel();
