import { createFormObject } from "../helpers/formatObject/formatObject.js";
import { postCreate } from "../helpers/Post/post-create.js";
import { html } from "../node_modules/lit-html/lit-html.js"

let create = async e => {
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
        postCreate(body);
    } catch (error) {

    }



}

export let createFurnitureTemplate = () => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Create New Furniture</h1>
        <p>Please fill all fields.</p>
    </div>
</div>
<form @submit="${create}">
    <div class="row space-top">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-make">Make</label>
                <input id="make" class="form-control is-valid" id="new-make" type="text" name="make">
            </div>
            <div class="form-group has-success">
                <label class="form-control-label" for="new-model">Model</label>
                <input id="model" class="form-control is-valid" id="new-model" type="text" name="model">
            </div>
            <div class="form-group has-danger">
                <label class="form-control-label" for="new-year">Year</label>
                <input id="year" class="form-control is-valid" id="new-year" type="number"
                    name="year">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-description">Description</label>
                <input id="description" class="form-control is-valid"
                    id="new-description" type="text" name="description">
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-price">Price</label>
                <input id="price" class="form-control is-valid" id="new-price"
                    type="number" name="price">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-image">Image</label>
                <input id="img" class="form-control is-valid" id="new-image" type="text"
                    name="img">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-material">Material (optional)</label>
                <input id="material" class="form-control" id="new-material" type="text" name="material">
            </div>
            <input type="submit" class="btn btn-primary" value="Create" />
        </div>
    </div>
</form>`