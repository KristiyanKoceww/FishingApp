import { useState } from "react"
import './LocationSearch.css'
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

const LocationSearch = ({ onCityFound }) => {
    const [cityName, setCityName] = useState('');

    const api = process.env.REACT_APP_WEATHER_API_KEY;

    const getLocation = (name) => {
        const url = `http://dataservice.accuweather.com/locations/v1/cities/search?apikey=${api}&q=${name}`
        fetch(url)
            .then(res => res.json())
            .then(res => res.find(l => l.Country.ID === 'BG'))
            .then(r => onCityFound({
                key: r.Key
            }))
            .catch((error) => {
                console.log(error);
            });
    }

    return (
        <div className="LocationSearch">
            <div className="inputandbutton">
                <TextField
                    label="Enter city name"
                    color="secondary"
                    value={cityName}
                    onChange={e => setCityName(e.target.value)}
                    color="success"
                />
            </div>
            <br />
            <div className="SearchDiv">
                <Button variant="filled" color="success" className="SearchButton" onClick={e => getLocation(cityName)}>Search</Button>
            </div>
        </div>
    )
}

export default LocationSearch