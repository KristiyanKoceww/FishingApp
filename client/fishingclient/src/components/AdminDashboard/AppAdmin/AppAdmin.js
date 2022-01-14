// import './AppAdmin.css'
// import React from 'react'
// import { fetchUtils, Admin, Resource } from 'react-admin'
// import restProvider from 'ra-data-simple-rest';

// import CitiesList from '../Cities/CitiesList'
// import CreateCity from '../Cities/CreateCity'
// import EditCity from '../Cities/EditCity';
// import CustomDataProvider from '../CustomProviders/CustomDataProvider';
// import provider from '../CustomProviders/provider';

// const AppAdmin = () => {
//     const jwt = localStorage.getItem("jwt");
//     const httpClient = (url, options = {}) => {
//         options.user = {
//             authenticated: true,
//             token: 'Bearer ' + jwt
//         };
//         return fetchUtils.fetchJson(url, options);
//     };

//     const dataProvider = restProvider('https://localhost:44366/api', httpClient);

//     return (
//         <Admin title='Admin panel' dataProvider={provider}>
//             <Resource name='Cities/getCities' list={CitiesList} create={CreateCity} edit={EditCity} /> 
//         </Admin>
//     )
// }

// export default AppAdmin

