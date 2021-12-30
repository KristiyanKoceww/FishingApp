import React from 'react'
import { fetchUtils, Admin, Resource } from 'react-admin'
import lb4Provider from 'react-admin-lb4'
import './AppAdmin.css'
import CitiesList from './Cities/CitiesList'
import UserList from './Cities/UserList'


import jsonServerProvider from 'ra-data-json-server';


const AppAdmin = () => {
    const jwt = localStorage.getItem("jwt");
    const httpClient = (url, options = {}) => {
        options.user = {
            authenticated: true,
            token: 'Bearer ' + jwt
        };
        return fetchUtils.fetchJson(url, options);
    };
    

    const dataProvider = jsonServerProvider('https://localhost:44366/api', httpClient);
     dataProvider.getList('Cities/getCities', {
        pagination: { page: 1, perPage: 15 },
        sort: { field: 'Name', order: 'ASC' },
        
    })
    .then(response => console.log(response));

    return (
        <Admin dataProvider={dataProvider}>
            <Resource name='Cities/getCities' list={CitiesList} />
            <Resource name='Votes/getCities' list={CitiesList} />
        </Admin>
    )
}

export default AppAdmin

