function attachEvents() {
    let submit = document.getElementById('submit');
    submit.addEventListener('click', fun)

}
function fun() {
    let input = document.getElementById('location');
    let code;
    let today;
    let upcoming;
    let weather = {
        Sunny: '☀',
        "Partly sunny": '⛅',
        Overcast: '☁',
        Rain: '☂'
    }

    fetch(`http://localhost:3030/jsonstore/forecaster/locations`)
        .then(body => body.json())
        .then(locations => {

            code = locations.find(x => x.name === input.value)
            console.log(code);
            fetch(`http://localhost:3030/jsonstore/forecaster/today/${code.code}`)
                .then(body => body.json())
                .then(todayWeather => {
                    today = todayWeather;
                    fetch(`http://localhost:3030/jsonstore/forecaster/upcoming/${code.code}`)
                        .then(body => body.json())
                        .then(upcomingWeather => {
                            upcoming = upcomingWeather;
                            document.getElementById('forecast').style.display = 'inline-block';

                            let label = document.getElementsByClassName('label')[0]


                            let forecast = document.createElement('div');
                            forecast.className = 'forecasts';

                            let specialSimbol = document.createElement('span');
                            specialSimbol.className = 'condition symbol';
                            specialSimbol.textContent = weather[today.forecast.condition];

                            let condition = document.createElement('span');
                            condition.className = 'condition';

                            let fd1 = document.createElement('span');
                            fd1.className = 'forecast-data';
                            fd1.textContent = today.name;

                            let fd2 = document.createElement('span');
                            fd2.className = 'forecast-data';
                            fd2.textContent = `${today.forecast.low}°/${today.forecast.high}°`;

                            let fd3 = document.createElement('span');
                            fd3.className = 'forecast-data';
                            fd3.textContent = today.forecast.condition;

                            condition.appendChild(fd1);
                            condition.appendChild(fd2);
                            condition.appendChild(fd3);

                            label.appendChild(forecast);
                            forecast.appendChild(specialSimbol);
                            forecast.appendChild(condition);

                            let upcoming1 = document.createElement('span');
                            upcoming1.className = 'upcoming';

                            let symbol1 = document.createElement('span');
                            symbol1.className = 'symbol';
                            symbol1.textContent = weather[upcoming.forecast[0].condition];

                            let temp1 = document.createElement('span');
                            temp1.className = 'forecast-data';
                            temp1.textContent = `${upcoming.forecast[0].low}°/${upcoming.forecast[0].high}°`;

                            let weather1 = document.createElement('span');
                            weather1.className = 'forecast-data';
                            weather1.textContent = upcoming.forecast[0].condition;

                            upcoming1.appendChild(symbol1);
                            upcoming1.appendChild(temp1);
                            upcoming1.appendChild(weather1);


                            let upcoming2 = document.createElement('span');
                            upcoming2.className = 'upcoming';

                            let symbol2 = document.createElement('span');
                            symbol2.className = 'symbol';
                            symbol2.textContent = weather[upcoming.forecast[1].condition];

                            let temp2 = document.createElement('span');
                            temp2.className = 'forecast-data';
                            temp2.textContent = `${upcoming.forecast[1].low}°/${upcoming.forecast[1].high}°`;

                            let weather2 = document.createElement('span');
                            weather2.className = 'forecast-data';
                            weather2.textContent = upcoming.forecast[1].condition;

                            upcoming2.appendChild(symbol2);
                            upcoming2.appendChild(temp2);
                            upcoming2.appendChild(weather2);

                            let upcoming3 = document.createElement('span');
                            upcoming3.className = 'upcoming';

                            let symbol3 = document.createElement('span');
                            symbol3.className = 'symbol';
                            symbol3.textContent = weather[upcoming.forecast[2].condition];

                            let temp3 = document.createElement('span');
                            temp3.className = 'forecast-data';
                            temp3.textContent = `${upcoming.forecast[2].low}°/${upcoming.forecast[2].high}°`;

                            let weather3 = document.createElement('span');
                            weather3.className = 'forecast-data';
                            weather3.textContent = upcoming.forecast[2].condition;

                            upcoming3.appendChild(symbol3);
                            upcoming3.appendChild(temp3);
                            upcoming3.appendChild(weather3);


                            let divForectInfo = document.createElement('div');
                            divForectInfo.className = 'forecast-info';

                            divForectInfo.appendChild(upcoming1);
                            divForectInfo.appendChild(upcoming2);
                            divForectInfo.appendChild(upcoming3);

                            document.getElementsByClassName('label')[1].appendChild(divForectInfo);

                        })
                })   .catch(err => {
                    let errorDiv = document.createElement('div');
                    errorDiv.classList.add('label');
                    errorDiv.textContent = 'Error';
                    currentForecastContainer.appendChild(errorDiv);
            })

        })
}

attachEvents();