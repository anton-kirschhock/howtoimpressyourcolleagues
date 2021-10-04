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

  private get BannerTitle(): HTMLHeadingElement {
    return document.querySelector("#banner-title") as HTMLHeadingElement;
  }
  private get BoxContainer(): HTMLDivElement {
    return document.querySelector("div.box-container") as HTMLDivElement;
  }
  private get RenderArea(): HTMLDivElement {
    return document.querySelector("div#renderArea") as HTMLDivElement;
  }
  private get NextFact(): HTMLButtonElement {
    return document.querySelector("button#btnNextFact") as HTMLButtonElement;
  }

  constructor(private isFirst = true) {
    this.NextFact.onclick = (event) => this.OnNextFact(event);
  }

  private async OnNextFact(event: Event) {
    event.preventDefault();
    this.isFirst = false;

    try {
      const resp = await fetch(`${this.baseUrl}/random`);
      const jsonBody = (await resp.json()) as Fact;
      console.log(jsonBody);
      this.BoxContainer.classList.remove("before-first-time-click");
      this.BannerTitle.innerText = "Impress them with this:";
      this.NextFact.innerText = "Let's Continue inpressing!";
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
    p.classList.add("title");

    const ul = document.createElement("ul") as HTMLUListElement;

    if (fact.description !== undefined && fact.description != null) {
      const descr = document.createElement("li") as HTMLLIElement;
      descr.innerText = `${fact.description}`;
      descr.classList.add("description");
      ul.appendChild(descr);
    }

    if (fact.readMoreLink) {
      const readMore = document.createElement("a") as HTMLAnchorElement;
      readMore.href = fact.readMoreLink;
      readMore.classList.add("read-more");
      readMore.text = "Read more";
      ul.appendChild(readMore);
    }

    this.RenderArea.appendChild(p);
    this.RenderArea.appendChild(ul);
  }
}

const pageModel = new IndexPageModel();
