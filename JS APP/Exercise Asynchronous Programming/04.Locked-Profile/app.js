function lockedProfile() {
    let count = 1;
    fetch('http://localhost:3030/jsonstore/advanced/profiles')
        .then(body => body.json())
        .then(info => {
            for (let pearson in info) {
                let divMain = document.getElementById('main');

                let divClass = document.createElement('div');
                divClass.className = 'profile';

                let img = document.createElement('img');
                img.src = './iconProfile2.png';
                img.className = 'userIcon';

                let labelLock = document.createElement('label');
                labelLock.textContent = 'Lock';

                let inputLock = document.createElement('input');
                inputLock.type = 'radio';
                inputLock.value = 'lock'
                inputLock.checked = true;
                inputLock.name = `user${count}Locked`;

                let labelUnlock = document.createElement('label');
                labelUnlock.textContent = 'Unlock';

                let inputUnlock = document.createElement('input');
                inputUnlock.type = 'radio';
                inputUnlock.name = `user${count}Locked`;
                inputUnlock.value = 'unlock';

                let br = document.createElement('br');
                let hr = document.createElement('hr');

                let labelUsername = document.createElement('label');
                labelUsername.textContent = 'Username';

                let name = document.createElement('input');
                name.type = 'text';
                name.text = `user${count}Username`;
                name.value = info[pearson].username;
                name.disabled = true;
                name.readOnly = true;

                let div = document.createElement('div');
                div.id = `user${count}HidenFields`;
                div.style.display = 'none';

                let labelEmail = document.createElement('label');
                labelEmail.textContent = 'Email:'

                let inputEmail = document.createElement('input');
                inputEmail.type = 'email';
                inputEmail.name = `user${count}Email`;
                inputEmail.value = info[pearson].email;
                inputEmail.disabled = true;
                inputEmail.readOnly = true;

                let labelAge = document.createElement('label');
                labelAge.textContent = 'Age:'

                let inputAge = document.createElement('input');
                inputAge.type = 'email';
                inputAge.name = `user${count}Age`;
                inputAge.value = info[pearson].age;
                inputAge.disabled = true;
                inputAge.readOnly = true;


                let button = document.createElement('button');
                button.textContent = 'Show more';
                button.addEventListener('click', e)



                divMain.appendChild(divClass);
                divClass.appendChild(img);
                divClass.appendChild(labelLock);
                divClass.append(inputLock);
                divClass.appendChild(labelUnlock);
                divClass.appendChild(inputUnlock);
                divClass.appendChild(br);
                divClass.appendChild(hr);
                divClass.appendChild(labelUsername);
                divClass.appendChild(name);
                divClass.appendChild(div);
                div.appendChild(hr);
                div.appendChild(labelEmail);
                div.appendChild(inputEmail);
                div.appendChild(labelAge);
                div.appendChild(inputAge);
                divClass.appendChild(button)
                count++;
                function e(e) {
                    let radioButton = e.target.parentElement.querySelector('input[type="radio"]:checked');
                    if (radioButton.value == 'lock') {
                        return;
                    } 
                    if (button.textContent == 'Show more') {
                        button.textContent = 'Hide it'
                    }else{
                        button.textContent = 'Show more'
                    }
                    if (div.style.display == 'block'){
                        div.style.display = 'none'
                    }else{
                        div.style.display ='block'
                    }
                   
                }


            }
        })
}