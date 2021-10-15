function solve() {

    function depart() {
        let stopID;
        let info = document.getElementById('info');
        let dBtn = document.getElementById('depart');
        let aBtn = document.getElementById('arrive');

        if (info.getAttribute('stop-id')) {
            stopID = info.getAttribute('stop-id');
        } else {
            stopID = 'depot';
        }
        fetch(`http://localhost:3030/jsonstore/bus/schedule/${stopID}`)
            .then(body => body.json())
            .then(next => {
                info.setAttribute('stop-id', next.next);
                info.setAttribute('name', next.name)
                info.textContent = `Next stop ${next.name}`;
                dBtn.disabled = true;
                aBtn.disabled = false;
            })
            .catch(e => {
                dBtn.disabled = aBtn.disabled = false;
                info.textContent = 'Error';
            })
    }

    function arrive() {
        let dBtn = document.getElementById('depart');
        let aBtn = document.getElementById('arrive');
        let info = document.getElementById('info');
        info.textContent = `Arriving at ${info.getAttribute('name')}`;
        aBtn.disabled = true;
        dBtn.disabled = false;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();