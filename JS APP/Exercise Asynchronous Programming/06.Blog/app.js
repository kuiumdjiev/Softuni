async function Solution() {
    let infoLoad = await fetch('http://localhost:3030/jsonstore/blog/posts')
    let dataLoad = await infoLoad.json();
    load(dataLoad)

    let infoView = await fetch('http://localhost:3030/jsonstore/blog/comments')
    let dataView = await infoView.json();
    view(dataLoad, dataView)
}
async function view(dataLoad, dataView) {
    let btnViewPost = document.getElementById('btnViewPost');

    btnViewPost.addEventListener('click', e => {
        let getTitle = document.getElementById('posts').value;
        console.log(getTitle)

        let postComments = document.getElementById('post-comments');

        while (postComments.firstChild) {
            postComments.removeChild(postComments.firstChild);
        }

        let title = document.getElementById('post-title');
        let p = document.getElementById('post-body');

        for (let info in dataLoad) {
            if (getTitle = dataLoad[info].id) {
                title.textContent = dataLoad[info].title;
                p.textContent = dataLoad[info].body;
                break;
            }
        }




        for (let info in dataView) {
            if (getTitle == dataView[info].postId) {
                let li = document.createElement('li');
                li.id = info.id;
                li.textContent = dataView[info].text;

                postComments.appendChild(li);
            }
        }
    })

}

async function load(data) {
    let btnLoadPosts = document.getElementById('btnLoadPosts');

    btnLoadPosts.addEventListener('click', e => {
        while (document.getElementById('posts').firstChild) {
            document.getElementById('posts').removeChild(document.getElementById('posts').firstChild)
        }
        for (let info in data) {
            let option = document.createElement('option');
            option.textContent = data[info].title;
            option.value = data[info].id;
            document.getElementById('posts').appendChild(option);
        }
    })
}

Solution();