import { Create, Edit, SimpleForm, DisabledInput, TextInput, DateInput, LongTextInput, ReferenceManyField, Datagrid, TextField, DateField, EditButton } from 'react-admin';

 const EditCity = (props) => (
    <Edit title='Edit city' {...props}>
        <SimpleForm>
            <TextInput source="Name" />
            <TextInput source="Description" options={{ multiLine: true }} />
            <TextInput source="CountryId" />
        </SimpleForm>
    </Edit>
);
export default EditCity