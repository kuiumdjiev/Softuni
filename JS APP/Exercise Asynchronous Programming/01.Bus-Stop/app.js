function getInfo() {
    let stopId = document.getElementById('stopId').value;
    fetch(`http://localhost:3030/jsonstore/bus/businfo/${stopId}`)
        .then(body => body.json())
        .then(info => {
            document.getElementById('stopName').textContent = info.name;
            let buses = document.getElementById('buses');
            while (buses.firstChild) {
                buses.removeChild(buses.firstChild);
            }
            Object.keys(info.buses).forEach(bus => {
                let li = document.createElement('li');
                li.textContent = `Bus ${bus} arrives in ${info.buses[bus]}`;
                buses.appendChild(li);
            })
        })
        .catch(e => {
            let buses = document.getElementById('buses');
            while (buses.firstChild) {
                buses.removeChild(buses.firstChild);
            }
            document.getElementById('stopName').textContent = 'Error';
        })
}