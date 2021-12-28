import { useEffect, useState } from "react";
import WeatherDay from './WeatherDay'
import LocationSearch from './LocationSearch'
import './FiveDaysWeatherForecast.css'

const FiveDaysWeatherForecast = () => {
    const [weather, setWeather] = useState();
    const [text, setText] = useState('');
    const [locationKey, setLocationKey] = useState();

    const api = process.env.REACT_APP_WEATHER_API_KEY;
   

    const padNUm = (num) => {
        const stringNUm = num + '';
        const stringLen = stringNUm.length;

        if (stringLen === 1) {
            return '0' + stringNUm
        }
        else {
            return stringNUm;
        }
    }

    useEffect(() => {
        const daysOfWeek = [
            'Sunday',
            'Monday',
            'Tuesday',
            'Wednesday',
            'Thurstday',
            'Friday',
            'Saturday',
        ]
        if (locationKey) {
            fetch(`http://dataservice.accuweather.com/forecasts/v1/daily/5day/${locationKey}?apikey=${api}&details=true&metric=true`)
                .then((res) => res.json())
                .then((result) => {
                   
                    setWeather(result.DailyForecasts.map(df => {
                        return {
                            max: df.Temperature.Maximum.Value,
                            min: df.Temperature.Minimum.Value,

                            airQuality: df.AirAndPollen[0].Category,

                            dayOfWeek: daysOfWeek[new Date(df.Date).getDay()],
                            weatherType: df.Day.IconPhrase,
                            clouds: df.Day.CloudCover,
                            rain: df.Day.Rain.Value,
                            rainProbability: df.Day.RainProbability,
                            snow: df.Day.Snow.Value,
                            snowUnit: df.Day.Snow.Unit,
                            thunderStormProbability: df.Day.ThunderstormProbability,
                            weatherKey: padNUm(df.Day.Icon),
                            windDirection: df.Day.Wind.Direction.English,
                            windSpeed: df.Day.Wind.Speed.Value,
                            windUnit: df.Day.Wind.Speed.Unit,
                            hoursOfSun: df.HoursOfSun,

                            cloudsNight: df.Night.CloudCover,
                            rainNight: df.Night.Rain.Value,
                            rainProbabilityNight: df.Night.RainProbability,
                            snowNight: df.Night.Snow.Value,
                            snowUnitNight: df.Night.Snow.Unit,
                            thunderStormProbabilityNight: df.Night.ThunderstormProbability,
                            windDirectionNight: df.Night.Wind.Direction.English,
                            windSpeedNight: df.Night.Wind.Speed.Value,
                            windUnitNight: df.Night.Wind.Speed.Unit,

                            minRealFeelTemp: df.RealFeelTemperature.Minimum.Value,
                            maxRealFeelTemp: df.RealFeelTemperature.Maximum.Value,
                        }
                    }));
                    setText(result.Headline.Text)
                });
        }
    }, [locationKey]);

    return (
        <div className="FiveDaysWeatherForecast">
            <LocationSearch
                onCityFound={cityKey => {
                    setLocationKey(cityKey.key)
                }}
            />
            <div className="infoText">{text}</div>
            <div className="WeatherInfo">
                {!!weather && weather.map((i, index) => (
                    <div className="day" key={index}>
                        <WeatherDay weather={i} />
                    </div>
                ))}
            </div>
        </div>
    )
}

export default FiveDaysWeatherForecast