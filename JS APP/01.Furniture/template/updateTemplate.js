import { createFormObject } from "../helpers/formatObject/formatObject.js";
import { putUpdate } from "../helpers/Put/put-update.js";
import { html } from "../node_modules/lit-html/lit-html.js"
let update = async e => {
    e.preventDefault();
    let formObject = createFormObject(e.target)

    //  Check(formObject.make, formObject.model, formObject.year, formObject.description, formObject.price, formObject.img)

    let make = formObject.make;
    let model = formObject.model;
    let material = formObject.material;
    let year = formObject.year;
    let description = formObject.description;
    let price = formObject.price;
    let img = formObject.img

    let body = {
        make,
        model,
        material,
        year,
        description,
        price,
        img
    };
    let wrnog = false;

    try {
        if (make.length < 4) {
            document.getElementById('make').className = 'form-control is-invalid';
            wrnog = true
        }

        if (model.length < 4) {
            document.getElementById('model').className = 'form-control is-invalid'
            wrnog = true
        }

        year = Number(year);
        if (!(year >= 1950 && year <= 2050)) {
            document.getElementById('year').className = 'form-control is-invalid'
            wrnog = true
        }

        if (description.length <= 10) {
            document.getElementById('description').className = 'form-control is-invalid'
            wrnog = true
        }

        price = Number(price);
        if (!(price > 0)) {
            document.getElementById('price').className = 'form-control is-invalid'
            wrnog = true
        }

        if (img.trim() === '') {
            document.getElementById('img').className = 'form-control is-invalid'
            wrnog = true
        }
        if (wrnog) {
            throw Error();
        }
        let id = document.getElementsByClassName("btn btn-info")[0];
        console.log(id);
        putUpdate(body, id.id);
    } catch (error) {

    }



}


export let updateFurnitureTemplate = (data) => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Edit Furniture</h1>
        <p>Please fill all fields.</p>
    </div>
</div>
<form @submit=${update}>
    <div class="row space-top" name="id" id="${data._id}">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-make">Make</label>
                <input class="form-control is-valid" id="make" type="text" name="make" .value=${data.make}>
            </div>
            <div class="form-group has-success">
                <label class="form-control-label" for="new-model">Model</label>
                <input class="form-control is-valid" id="model" type="text" name="model" .value=${data.model}>
            </div>
            <div class="form-group has-danger">
                <label class="form-control-label" for="new-year">Year</label>
                <input class="form-control is-valid" id="year" type="number" name="year" .value=${data.year}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-description">Description</label>
                <input class="form-control is-valid" id="description" type="text" name="description"
                    .value=${data.description}>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-price">Price</label>
                <input class="form-control is-valid" id="price" type="number" name="price" .value=${data.price}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-image">Image</label>
                <input class="form-control is-valid" id="image" type="text" name="img" .value=${data.img}>
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-material">Material (optional)</label>
                <input class="form-control" id="material" type="text" name="material" .value=${data.material}>
            </div>
            <input type="submit" class="btn btn-info" id="${data._id}" value="Edit" />
        </div>
    </div>
</form>`