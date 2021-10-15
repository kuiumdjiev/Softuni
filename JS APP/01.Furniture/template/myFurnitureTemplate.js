import { html } from "../node_modules/lit-html/lit-html.js"

export let furniture = (data) => html`
<div class="col-md-4">
    <div class="card text-white bg-primary">
        <div class="card-body">
            <img src=${data.img} />
            <p>Description here</p>
            <footer>
                <p>Price: <span>${data.price} $</span></p>
            </footer>
            <div>
                <a href="/detailsFurniture/${data._id}" class="btn btn-info">Details</a>
            </div>
        </div>
    </div>
</div>`

export let myFurnitureTemplate=(data)=>html`
 <div class="container">
        <div class="row space-top">
            <div class="col-md-12">
                <h1>My Furniture</h1>
                <p>This is a list of your publications.</p>
            </div>
        </div>
        ${data.map(x=>furniture(x))}
        </div>
    </div>`