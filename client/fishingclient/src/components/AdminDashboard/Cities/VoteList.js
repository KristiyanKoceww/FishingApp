import React from "react";
import { List, Datagrid, TextField} from 'react-admin'

const VoteList = (props) => {

    return(
        <List {...props}>
            <Datagrid>
                <TextField source="Id"></TextField>
                <TextField source="UserId"></TextField>
            </Datagrid>
        </List>
    )
}

export default VoteList