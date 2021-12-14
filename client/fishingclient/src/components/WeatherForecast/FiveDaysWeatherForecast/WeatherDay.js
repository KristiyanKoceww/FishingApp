import './WeatherDay.css'
const WeatherDay = (props) => {
    console.log(props);
    return (
        <div className='WeatherDay'>
            <div className="dayOfWeek">
                {props.weather.dayOfWeek}
                <img className='weatherIcon' alt={props.weather.weatherType} src={`https://developer.accuweather.com/sites/default/files/${props.weather.weatherKey}-s.png`} />
            </div>
            <div>Temp: min {props.weather.min} / max {props.weather.max}</div>
            <div>Real feel : min: {props.weather.minRealFeelTemp} / max: {props.weather.maxRealFeelTemp}</div>
            <div>Wind: {props.weather.windSpeed} {props.weather.windUnit} {props.weather.windDirection}</div>
            <div>Rain probability: {props.weather.rainProbability} %</div>
            <div>Rain: {props.weather.rain} mm</div>
            <div>Thunder storm probability: {props.weather.thunderStormProbability} %</div>
            <div>Clouds: {props.weather.clouds} %</div>
            <div>Hours of sun: {props.weather.hoursOfSun} h</div>
            <div>Snow: {props.weather.snow} {props.weather.snowUnit}</div>
        </div>
    )
}

export default WeatherDay