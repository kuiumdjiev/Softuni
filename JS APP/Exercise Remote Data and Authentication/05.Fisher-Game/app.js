let loadButton = document.querySelector('main aside .load');
loadButton.addEventListener('click', loadFun);

let catchesContainer = document.getElementById('catches');

let addButton = document.querySelector('#addForm .add');
addButton.disbled = localStorage.getItem('token') == null;
addButton.addEventListener('click', addFun);

async function addFun() {
    let id = localStorage.getItem('id');
    let anglert = document.getElementsByClassName('angler');
    let weight = document.getElementsByClassName('weight');
    let species = document.getElementsByClassName('species');
    let location = document.getElementsByClassName('location');
    let bait = document.getElementsByClassName('bait');
    let captureTime = document.getElementsByClassName('captureTime');

    let cathc = {
        angler: anglert.value,
        weight: Number(weight.value),
        species: species.value,
        location: location.value,
        bait: bait.value,
        captureTime: Number(captureTime.value)
    }

    let method = {
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': localStorage.getItem('token')
        },
        method: 'Put',
        body: JSON.stringify(cathc)
    };

    let url = 'http://localhost:3030/data/catches';
    fetch(url, method)
    loadFun();
}

async function loadFun() {


    catchesContainer.querySelectorAll('.catch').forEach(x => x.remove());
    let url = `http://localhost:3030/data/catches`;
    let response = await fetch(url);
    let result = await response.json();
    for (let $catch in result) {
        let ownerId = result[$catch]._ownerId;
        let id = result[$catch]._id;
        let angler = result[$catch].angler;
        let weight = result[$catch].weight;
        let species = result[$catch].species;
        let location = result[$catch].location;
        let bait = result[$catch].bait;
        let captureTime = result[$catch].captureTime;
        createCathc(ownerId, id, angler, weight, species, location, bait, captureTime)
    }

}

function createCathc(ownerId, id, angler, weight, species, location, bait, captureTime) {
    let div = ce('div', { class: 'catch' }, undefined);

    let labelAngler = ce('label', undefined, 'Angler');
    let inputAngler = ce('input', { type: 'text', class: 'angler', }, angler);

    let hr = ce('hr', undefined, undefined);

    let labelWeight = ce('label', undefined, 'Weight');
    let inputWeight = ce('input', { type: 'number', class: 'weight', textContent: weight });

    let labelSpecies = ce('label', undefined, 'Species');
    let inputSpecies = ce('input', { type: 'text', class: 'species', textContent: species });

    let labelLocation = ce('label', undefined, 'Location');
    let inputLocation = ce('input', { type: 'text', class: 'location', textContent: location });

    let labelBait = ce('label', undefined, 'Bait');
    let inputBait = ce('input', { type: 'text', class: 'bait', textContent: bait });

    let labelCapturTime = ce('label', undefined, 'Capture Time');
    let inputCaptureTime = ce('input', { type: 'number', class: 'captureTime', textContent: captureTime });

    let buttonUpdate = ce('button', { class: 'update', disbled: localStorage.getItem('id') != ownerId }, 'Update');
    buttonUpdate.addEventListener('click', Update);

    let buttonDelete = ce('button', { class: 'delete', disbled: localStorage.getItem('id') != ownerId }, 'Delete');
    buttonDelete.addEventListener('click', Delete);

    div.dataset.id = id;
    div.dataset.ownerId = ownerId;

    div.appendChild(labelAngler);
    div.appendChild(inputAngler);

    div.appendChild(hr);

    div.appendChild(labelWeight);
    div.appendChild(inputWeight);

    div.appendChild(hr);

    div.appendChild(labelSpecies);
    div.appendChild(inputSpecies);

    div.appendChild(hr);

    div.appendChild(labelLocation);
    div.appendChild(inputLocation);

    div.appendChild(hr);

    div.appendChild(labelBait);
    div.appendChild(inputBait);

    div.appendChild(hr);

    div.appendChild(labelCapturTime);
    div.appendChild(inputCaptureTime);

    div.appendChild(buttonUpdate);
    div.appendChild(buttonDelete);
    catchesContainer.appendChild(div);
}

function ce(tag, attributes, ...params) {
    let element = document.createElement(tag);
    let firstValue = params[0];
    if (params.length === 1 && typeof (firstValue) !== 'object') {
        if (['input', 'textarea'].includes(tag)) {
            element.value = firstValue;
        } else {
            element.textContent = firstValue;
        }
    } else {
        element.append(...params);
    }

    if (attributes !== undefined) {
        Object.keys(attributes).forEach(key => {
            element.setAttribute(key, attributes[key]);
        })
    }

    return element;
}
async function Delete(e) {
    let target = e.target.parentElement;
    let id = target.dataset.id;
    let url = `http://localhost:3030/data/catches/${id}`;
    let method = {
        headers: {
            'X-Authorization': localStorage.getItem('token')
        },
        method: 'Delete'
    }
    fetch(url, method);
}

async function Update(e) {
    let id = localStorage.getItem('id');
    let anglert = document.getElementsByClassName('angler');
    let weight = document.getElementsByClassName('weight');
    let species = document.getElementsByClassName('species');
    let location = document.getElementsByClassName('location');
    let bait = document.getElementsByClassName('bait');
    let captureTime = document.getElementsByClassName('captureTime');

    let cathc = {
        angler: anglert.value,
        weight: Number(weight.value),
        species: species.value,
        location: location.value,
        bait: bait.value,
        captureTime: Number(captureTime.value)
    }

    let method = {
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': localStorage.getItem('token')
        },
        method: 'Put',
        body: JSON.stringify(cathc)
    };

    fetch(url, method)
    loadFun();

}