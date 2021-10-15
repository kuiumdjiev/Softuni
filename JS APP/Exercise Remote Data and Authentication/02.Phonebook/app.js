async function attachEvents() {
    let person = document.getElementById('person');
    let phone = document.getElementById('phone');
    let loadBtn = document.getElementById('btnLoad');
    let createBtn = document.getElementById('btnCreate');
    let url = 'http://localhost:3030/jsonstore/phonebook';


    loadBtn.addEventListener('click', loadFun);
    createBtn.addEventListener('click', createFun)

    async function loadFun() {
        while (document.getElementById('phonebook').firstChild) {
            document.getElementById('phonebook').removeChild(document.getElementById('phonebook').firstChild);
        }
        let $fetch = await fetch(url);
        let body = await $fetch.json();
        for (let key in body) {
            let li = document.createElement('li');
            li.textContent = `${body[key].person}: ${body[key].phone}`
            let deleteBtn = document.createElement('button')
            deleteBtn.textContent = 'Delete';
            deleteBtn.id = key;
            deleteBtn.addEventListener('click', deleteFun)

            li.appendChild(deleteBtn);
            document.getElementById('phonebook').appendChild(li);
        }
    }
    async function createFun() {
        let method = {
            method: "POST",
            senders: { 'Content-type': 'aplication/json' },
            body: JSON.stringify({
                person: person.value,
                phone: phone.value
            })
        };
        fetch(url, method)
        loadFun();


    }
    function deleteFun() {
        let urlDelete = `http://localhost:3030/jsonstore/phonebook/${this.id}`;
        let method = {
            method: 'DELETE'
        }
        fetch(urlDelete, method);
    }

}

attachEvents();