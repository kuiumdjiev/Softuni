let firstName = document.getElementById('firstName');
let lastName = document.getElementById('lastName');
let facultyNumber = document.getElementById('facultyNumber');
let grade = document.getElementById('grade');
let submitBtn = document.getElementById('submit');
let url = `http://localhost:3030/jsonstore/collections/students`;
loadFun();
submitBtn.addEventListener('click', submitFun);

function loadFun() {
    let method = {
        method: "GET",
        headers: { 'Content-type': 'application/json' },

    };
    fetch(url, method)
        .then(body => body.json())
        .then(pearsons => {
            for (let pearson in pearsons) {
                let thead = document.createElement('thead');
                let tr = document.createElement('tr');
                add(tr, pearsons[pearson].firstName);
                add(tr, pearsons[pearson].lastName);
                add(tr, pearsons[pearson].facultyNumber);
                add(tr, pearsons[pearson].grade);
                let tbody = document.createElement('tbody')
                tbody.appendChild(tr);

                document.getElementById('results').appendChild(tbody);

            }
        })
}
function add(tr, th) {
    let $th = document.createElement('th');
    $th.textContent = th;
    tr.appendChild($th);
}


function submitFun() {
    check();
    let method = {
        method: "POST",
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify({
            facultyNumber: facultyNumber.value,
            firstName: firstName.value,
            grade: grade.value,
            lastName: lastName.value
        })
    };
    fetch(url, method)

}


function check() {
    if (!firstName.value || !lastName.value || !facultyNumber.value || !grade.value) {
        return;
    } if (typeof firstName != 'string' || typeof lastName != 'string' || typeof facultyNumber != 'string' || typeof grade != 'number') {
        return;
    }
}

