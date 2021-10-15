
export function views(section) {

    loadFun(section);
}
function loadFun(section) {
    let url = 'http://localhost:3030/jsonstore/collections/myboard/posts';
    fetch(url)
        .then(body => body.json())
        .then(body => {
            for (let x in body) {
                let title = body[x].title;
                let date = body[x].time;
                let username = body[x].username;
                let id = body[x]._id;
                create(title, date, username, id, section);
            }

        })
}
function post(form) {

    form.addEventListener('submit', createPost);

    let cancelButton = form.querySelector('.cancel');
    cancelButton.addEventListener('click', form.reset());
}
function createPost(e) {

    e.preventDefault();

    let form = e.target;
    let data = new FormData(form);

    let title = data.get('topicName');
    let username = data.get('username')
    let post = data.get('postText');

    let check = [title, username, post, data];
    if (check.some(x => x === '')) {
        alert('Required fields cannot be empty!');
        return;
    }

    let body = {
        title,
        username,
        post,
        time: getTimeHomeFormat()
    }
    e.target.reset();


    let url = 'http://localhost:3030/jsonstore/collections/myboard/posts';
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    });
    let titles = document.getElementsByClassName('topic-container')[0];
    while (titles.firstChild) {
        titles.removeChild(titles.firstChild);
    }
    loadFun(titles)
}

function getTimeHomeFormat() {
    let time = new Date();
    let year = time.getFullYear()
    let month = time.getMonth()
        .toString()
        .padStart(2, 0);

    let day = time.getDay()
        .toString()
        .padStart(2, 0);

    let hours = time.getHours() > 12 ? (time.getHours() - 12).toString().padStart(2, 0)
        : (time.getHours()).toString().padStart(2, 0);
    let minutes = time.getMinutes()
        .toString()
        .padStart(2, 0);

    let seconds = time.getHours()
        .toString()
        .padStart(2, 0);

    let miliseconds = time.getMilliseconds()
        .toString()
        .padStart(3, 0);

    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}.${miliseconds}Z`;
}
function create(title, date, username, id, section) {
    let topicNameWrapper = ct('div', { class: 'topic-name-wrapper' });

    let topicName = ct('div', { class: 'topic-name' });

    let a = ct('a', { href: '#', id: id, class: 'normal' });
    a.addEventListener('click', open);

    let h2 = ct('h2', undefined, title);

    let colums = ct('div', { class: 'colums' });

    let simple = ct('div', undefined, undefined);

    let dateP = ct('p', undefined, 'Date: ');

    let time = ct('time', undefined, date);

    let usernameP = ct('p', { class: 'nick-name' }, 'Username: ');

    let span = ct('span', undefined, username);

    dateP.appendChild(time);
    usernameP.appendChild(span);

    simple.appendChild(dateP);
    simple.appendChild(usernameP);

    colums.appendChild(simple);

    a.appendChild(h2);

    topicName.appendChild(a);
    topicName.appendChild(colums);

    topicNameWrapper.appendChild(topicName);

    section.appendChild(topicNameWrapper);

}
function ct(tag, attributes, ...params) {
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
function open(e) {
    e.preventDefault();
    let target = e.target.parentElement;
    localStorage.setItem('id', target.id);

    location.assign('theme-content.html');
}


let tools = {
    views,
    post
}
export default tools;