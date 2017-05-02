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
  ReferenceManyField,
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
export const CategoryList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.CategoryName}
          secondaryText={record => record.Description}
        />
      }
      medium={
        <Datagrid>
          <TextField source="id"/>
          <TextField source="CategoryName" label="Category" style={titleFieldStyle}/>
          <TextField source="Description" style={{fontStyle: 'italic'}}/>
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const CategoryTitle = translate(({record, translate}) => {
  return <span>Category</span>;
});

export const CategoryCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['CategoryName', 'Description'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <TextInput source="CategoryName"/>
      <LongTextInput source="Description" options={{multiLine: true}}/>
    </SimpleForm>
  </Create>
);

export const CategoryEdit = ({...props}) => (
  <Edit title={<CategoryTitle />} {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <TextInput source="CategoryName" label="Category" validate={required}/>
      <LongTextInput source="Description" validate={required}/>
    </SimpleForm>
  </Edit>
);

export const CategoryShow = ({...props}) => (
  <Show title={<CategoryTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="CategoryName" label="Category"/>
      <TextField source="Description"/>
      <ReferenceManyField label="Products" reference="products" target="CategoryId">
        <Datagrid selectable={false}>
          <TextField source="Name"/>
          <TextField source="Price"/>
          <TextField source="Number"/>
          <ShowButton />
        </Datagrid>
      </ReferenceManyField>
    </SimpleShowLayout>
  </Show>
);
