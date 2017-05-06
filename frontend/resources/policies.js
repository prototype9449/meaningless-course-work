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
export const PolicyList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.TableName}
          secondaryText={record => record.PredicateValue}
        />
      }
      medium={
        <Datagrid>
          <TextField source="id"/>
          <TextField source="TableName" label="Table" style={titleFieldStyle}/>
          <TextField source="PredicateValue" label="Predicate" />
          <TextField source="GroupIds" label="Identifiers of groups" />
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const PolicyTitle = translate(({record, translate}) => {
  return <span>Policy</span>;
});

export const PolicyCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['TableName', 'PredicateValue', 'GroupIds'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <TextInput source="TableName" label="Table" validate={required}/>
      <TextInput source="PredicateValue" label="Predicate" validate={required}/>
      <TextInput source="GroupIds" label="Identifiers of groups" />
    </SimpleForm>
  </Create>
);

export const PolicyEdit = ({...props}) => (
  <Edit title={<PolicyTitle />} {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <TextInput source="TableName" label="Table" validate={required}/>
      <TextInput source="PredicateValue" label="Predicate" validate={required}/>
      <TextInput source="GroupIds" label="Identifiers of groups" />
    </SimpleForm>
  </Edit>
);

export const PolicyShow = ({...props}) => (
  <Show title={<PolicyTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="TableName" label="Table"/>
      <TextField source="PredicateValue" label="Predicate"/>
      <TextField source="GroupIds" label="Identifiers of groups" />
    </SimpleShowLayout>
  </Show>
);
