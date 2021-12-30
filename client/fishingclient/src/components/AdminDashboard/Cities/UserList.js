import {
    List,
    Datagrid,
    TextField,
    EmailField,
    UrlField,
    Edit,
    SimpleForm,
    TextInput,
    Create,
} from 'react-admin';

const UserList = props => {
    console.log(props)
    return (
        <List {...props}>
            <Datagrid rowClick="edit">
                <TextField source="id" />
                <TextField source="name" />
                <TextField source="username" />
                <EmailField source="email" />
                <TextField source="address.street" label="Address" />
                <TextField source="phone" />
                <UrlField source="website" />
                <TextField source="company.name" label="Company" />
            </Datagrid>
        </List>
    )
}

export default UserList;