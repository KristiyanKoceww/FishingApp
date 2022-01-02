import React from "react";
import { List, Datagrid, TextField, EditButton, DeleteButton} from 'react-admin'

const CitiesList = (props) => {
console.log(props);
    return(
        <List {...props}>
            <Datagrid>
                <TextField source="id"></TextField>
                 <TextField source="name"></TextField>
                <TextField source="description"></TextField>
                <TextField source="countryName"></TextField>
                <TextField source="countryId"></TextField>
                <EditButton  basePath="/Cities/update?cityId="/>
                <DeleteButton basePath="Cities/delete?cityId="/>
            </Datagrid>
        </List>
    )
}

export default CitiesList