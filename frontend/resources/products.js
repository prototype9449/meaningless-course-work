import React from 'react';
import {
  BooleanField,
  BooleanInput,
  CheckboxGroupInput,
  Create,
  Datagrid,
  DateField,
  DateInput,
  DisabledInput,
  Edit,
  EditButton,
  Filter,
  FormTab,
  ImageField,
  ImageInput,
  List,
  LongTextInput,
  NumberField,
  NumberInput,
  ReferenceField,
  Responsive,
  RichTextField,
  SelectField,
  SelectInput,
  Show,
  ShowButton,
  SimpleForm,
  SimpleList,
  SimpleShowLayout,
  TabbedForm,
  TextField,
  TextInput,
  minValue,
  number,
  required,
  translate,
} from '../src/index';

const titleFieldStyle  = {maxWidth: '20em', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'};
export const ProductList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.Name}
          secondaryText={record => record.Price}
        />
      }
      medium={
        <Datagrid>
          <TextField source="id"/>
          <TextField source="Name" style={titleFieldStyle}/>
          <TextField source="Price" style={{fontStyle: 'italic'}}/>
          <NumberField source="CategoryId" label="Id of category" style={{fontStyle: 'italic'}}/>
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const ProductTitle = translate(({record, translate}) => {
  return <span>Product</span>;
});

export const ProductCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['Name', 'Price'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <TextInput source="Name" validate={required}/>
      <TextInput source="Price" validate={required}/>
      <NumberInput source="Number" validate={required}/>
      <NumberInput source="CategoryId"/>
    </SimpleForm>
  </Create>
);

export const ProductEdit = ({...props}) => (
  <Edit title={<ProductTitle />} {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <TextInput source="Name" validate={required}/>
      <TextInput source="Price" validate={required}/>
      <NumberInput source="Number" validate={required}/>
      <NumberInput source="CategoryId"/>
    </SimpleForm>
  </Edit>
);

export const ProductShow = ({...props}) => (
  <Show title={<ProductTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="Name"/>
      <TextField source="Price"/>
      <TextField source="Number"/>
      <NumberField source="CategoryId" label="Id of category"/>
    </SimpleShowLayout>
  </Show>
);
