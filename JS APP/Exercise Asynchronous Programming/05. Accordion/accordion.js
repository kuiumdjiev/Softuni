async function solution() {
    let info = await fetch('http://localhost:3030/jsonstore/advanced/articles/list')
    let data = await info.json();
    slove(data)
}
solution();
async function slove(info) {

    for (const element in info) {
        let divAccordion = document.createElement('div');
        divAccordion.className = 'accordion';

        let head = document.createElement('div');
        head.className = 'head';

        let title = document.createElement('span');
        title.textContent = info[element].title;

        let button = document.createElement('button');
        button.className = 'button';
        button.textContent = 'More';
        button.id = info[element]._id;

        let divExtra = document.createElement('div');
        divExtra.className = 'extra';

        let p = document.createElement('p');

        let infoP = await fetch(`http://localhost:3030/jsonstore/advanced/articles/details/${button.id}`);
        let dataP = await infoP.json();
        p.textContent = dataP.content;



        divAccordion.appendChild(head);
        divAccordion.appendChild(divExtra);
        head.appendChild(title);
        head.appendChild(button);
        divExtra.appendChild(p);
        divExtra.style.display = 'none';
        document.getElementById('main').appendChild(divAccordion);

        button.addEventListener('click', e => {
            if (divExtra.style.display == 'none') {
                button.textContent = 'Less';
                divExtra.style.display = 'block'
            } else {
                button.textContent = 'More';
                divExtra.style.display = 'none'
            }
        })

    }

}