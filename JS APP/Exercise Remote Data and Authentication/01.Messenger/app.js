async function attachEvents() {
    let textArea = document.getElementById('messages');
    let author = document.getElementById('author');
    let content = document.getElementById('content');
    let sumbmitBtn = document.getElementById('submit');
    let refreshBtn = document.getElementById('refresh');
    let url = 'http://localhost:3030/jsonstore/messenger';


    refresh();

    sumbmitBtn.addEventListener('click', sumbmit);
    refreshBtn.addEventListener('click', refresh)

    function sumbmit() {
         if (!author.value || !content.value) {
         return;
        }
        let post = {
            method: "POST",
            senders: { 'Content-type': 'aplication/json' },
            body: JSON.stringify({
                author: author.value,
                content: content.value
            })
        };
        fetch(url, post);
        author.value = '';
        content.value = '';
      
    }


    async function refresh() {
        textArea.textContent = '';
        let $fetch = await fetch(url);
        let $body = await $fetch.json();
        let values = Object.values($body);
        for (let text in values) {
            textArea.textContent += `${values[text].author}: ${values[text].content}\n`;
        }
    }




}

attachEvents();