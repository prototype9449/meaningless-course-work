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

const titleFieldStyle        = {maxWidth: '20em', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'};
export const OrderDetailList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.OrderId}
          secondaryText={record => record.ProductID}
        />
      }
      medium={
        <Datagrid>
          <TextField source="OrderId" label="Order Id" style={titleFieldStyle}/>
          <TextField source="ProductID" label="Product Id"/>
          <TextField source="Number"/>
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const OrderDetailTitle = translate(({record, translate}) => {
  return <span>Order Details</span>;
});

export const OrderDetailCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['OrderId', 'ProductID', 'Number'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <NumberInput source="OrderId" label="Order Id" validate={required}/>
      <NumberInput source="ProductID" label="Product Id" validate={required}/>
      <NumberInput source="Number" validate={required}/>
    </SimpleForm>
  </Create>
);

export const OrderDetailEdit = ({...props}) => (
  <Edit title={<OrderDetailTitle />} {...props}>
    <SimpleForm>
      <NumberInput source="OrderId" label="Order Id" validate={required}/>
      <NumberInput source="ProductID" label="Product Id" validate={required}/>
      <NumberInput source="Number" validate={required}/>
    </SimpleForm>
  </Edit>
);

export const OrderDetailShow = ({...props}) => (
  <Show title={<OrderDetailTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="OrderId" label="Order Id"/>
      <TextField source="ProductID" label="Product Id"/>
      <TextField source="Number"/>
    </SimpleShowLayout>
  </Show>
);
