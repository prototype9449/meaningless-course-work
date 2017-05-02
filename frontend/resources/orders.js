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
export const OrderList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.CustomerID}
          secondaryText={record => record.EmployeeID}
        />
      }
      medium={
        <Datagrid>
          <TextField source="id"/>
          <TextField source="CustomerID" label="Customer Id" style={titleFieldStyle}/>
          <TextField source="EmployeeID" label="Employee Id" />
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const OrderTitle = translate(({record, translate}) => {
  return <span>Order</span>;
});

export const OrderCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['CustomerId', 'EmployeeId'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <NumberInput source="CustomerID" label="Customer Id" validate={required}/>
      <NumberInput source="EmployeeID" label="Employee Id" validate={required}/>
    </SimpleForm>
  </Create>
);

export const OrderEdit = ({...props}) => (
  <Edit title={<OrderTitle />} {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <NumberInput source="CustomerID" label="Customer Id" validate={required}/>
      <NumberInput source="EmployeeID" label="Employee Id" validate={required}/>
    </SimpleForm>
  </Edit>
);

export const OrderShow = ({...props}) => (
  <Show title={<OrderTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="CustomerID" label="Customer Id"/>
      <TextField source="EmployeeID" label="Employee Id"/>
    </SimpleShowLayout>
  </Show>
);
