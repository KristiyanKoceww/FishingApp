import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./Weather.css";
import Footer from '../../Footer/Footer'
const Weather = () => {
  const api = {
    base: "https://api.openweathermap.org/data/2.5/",
    wholeLink: "https://api.openweathermap.org/data/2.5/forecast?q={city name}&units=metric&appid={API key}"
  };
  const apiKey = process.env.REACT_APP_WEATHER;
  const [query, setQuery] = useState("");
  const [weather, setWeather] = useState({});

  const search = (evt) => {
    if (evt.key === "Enter") {
      fetch(`${api.base}forecast?q=${query}&units=metric&lang=bg&appid=${apiKey}`)
        .then((res) => res.json())
        .then((result) => {
          setWeather(result);
          console.log(result);
          setQuery("");
        });
    }
  };

  const dateBuilder = (d) => {
    let months = [
      "Януари",
      "Февруари",
      "Март",
      "Април",
      "Май",
      "Юни",
      "Юли",
      "Август",
      "Септември",
      "Октомври",
      "Ноември",
      "Декември",
    ];
    let days = [
      "Неделя",
      "Понеделник",
      "Вторник",
      "Сряда",
      "Четвъртък",
      "Петък",
      "Събота",
    ];

    let day = days[d.getDay()];
    let date = d.getDate();
    let month = months[d.getMonth()];
    let year = d.getFullYear();

    return `${day} ${date} ${month} ${year}`;
  };

  return (
    <div className="mainWeather" >
      <main className={(typeof weather.city != "undefined") ? ((weather.list[0].main.temp > 16) ? 'warm' : 'cold') : 'warm'}>
        <h1 className="title">Времето</h1>
        <div className="search-box">
          <input
            type="text"
            className="search-bar"
            placeholder="Търси..."
            onChange={(e) => setQuery(e.target.value)}
            value={query}
            onKeyPress={search}
          />
        </div>
        {typeof weather.city != "undefined" ? (
          <div>
            <div className="location-box">
              <div className="location">
                {weather.city.name}, {weather.city.country}
              </div>
              <div className="date">{dateBuilder(new Date())}</div>
            </div>
            <div className="weather-box">
              <div className="temp">{Math.round(weather.list[0].main.temp)}°c , {weather.list[0].weather[0].description} </div>
              <div className="weather">{weather.list[0].weather.description}</div>
              <div className="real-feel">Усеща се : {weather.list[0].main.feels_like} °c</div>
            </div>
            <div className="weather-info">
              <div className="weather-info2">
                <div >Облачност : {weather.list[0].clouds.all} %</div>
                <div >Влажност : {weather.list[0].main.humidity} % </div>
                <div >Атмосферно налягане : {weather.list[0].main.pressure} хПа. </div>
                <div >Максимална температура : {weather.list[0].main.temp_max}  °c </div>
                <div >Минимална температура : {weather.list[0].main.temp_min}  °c</div>
                <div >Вятър : {weather.list[0].wind.speed} м/с. </div>
                <div >Видимост : {weather.list[0].visibility} м. </div>
                <div><Link to="/FiveDaysWeatherForecast" className="fivedays">Click here for 5 days weather forecast</Link></div>
              </div>
            </div>
          </div>
        ) : (
          <div className="empty-body">
            Моля, въведете град или държава за да разберете времето
          </div>

        )}
        <div className="footer2">
          <Footer />
        </div>
      </main>
    </div>
  );
};

export default Weather;
