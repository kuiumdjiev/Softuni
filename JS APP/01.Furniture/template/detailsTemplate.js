import { deleteFurniture } from "../helpers/Delete/delete-furniture.js";
import { html,nothing } from "../node_modules/lit-html/lit-html.js"
let $delete = async e => {
    e.preventDefault();
    let id =e.target.id;
    deleteFurniture(id);
}
export let detailsFurnitureTemplate = (details) => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Furniture Details</h1>
    </div>
</div>
<div class="row space-top">
    <div class="col-md-4">
        <div class="card text-white bg-primary">
            <div class="card-body">
                <img src="${details.img}" />
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <p>Make: <span>${details.make}</span></p>
        <p>Model: <span>${details.model}</span></p>
        <p>Year: <span>${details.year}</span></p>
        <p>Description: <span>${details.description}</span></p>
        <p>Price: <span>${details.price}</span></p>
        <p>Material: <span>${details.material}</span></p>
     ${sessionStorage.getItem('id')===details._ownerId?admin(details):nothing}
    </div>
</div>
`
export let admin = (data) => html`
<div>
    <a href="/updateFurniture/${data._id}" class="btn btn-info">Edit</a>
    <a @click="${$delete}"  id="${data._id}" class="btn btn-red">Delete</a>
</div>
`