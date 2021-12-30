import React from "react";
import { List, Datagrid, TextField} from 'react-admin'

const CitiesList = (props) => {

    return(
        <List {...props}>
            <Datagrid>
                <TextField source="Id"></TextField>
                <TextField source="Name"></TextField>
                <TextField source="Description"></TextField>
                <TextField source="CountryName"></TextField>
                <TextField source="CountryId"></TextField>
            </Datagrid>
        </List>
    )
}

export default CitiesList