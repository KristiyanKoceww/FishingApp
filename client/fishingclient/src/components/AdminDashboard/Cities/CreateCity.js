import { Create, Edit, SimpleForm, DisabledInput, TextInput, DateInput, LongTextInput, ReferenceManyField, Datagrid, TextField, DateField, EditButton } from 'react-admin';

 const CreateCity = (props) => (
    <Create  {...props}>
        <SimpleForm title='Create new city'>
            <TextInput source="Name" />
            <TextInput source="Description" options={{ multiLine: true }} />
            <TextInput source="CountryId" />
        </SimpleForm>
    </Create>
);
export default CreateCity