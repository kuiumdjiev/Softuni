import { html } from "./../node_modules/lit-html/lit-html.js";

export let catSingleTemplate = (cat) => html` 
<li>
    <img src="./images/${cat.imageLocation}.jpg" width="250" height="250" alt="Card image cap">
    <div class="info">
        <button class="showBtn" @click=${event} > Show status code</button>
        <div class="status" style="display: none" id=${cat.id}>
            <h4>${cat.statusCode}</h4>
            <p>${cat.statusMessage}</p>
        </div>
    </div>
</li>`;

export let catsTemplate = (cats) => html`
    <ul>
        ${cats.map(cat=>catSingleTemplate(cat))}
    </ul>
`;

let event = (e) => {
    let div = e.target.closest(".info").querySelector(".status");
    e.target.innerText = e.target.innerText.includes("Show")
      ? "Hide status code"
      : "Show status code";
    div.style.display = div.style.display === "none" ? "block" : "none";
}