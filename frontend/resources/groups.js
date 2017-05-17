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
import RichTextInput from 'aor-rich-text-input';
import Chip from 'material-ui/Chip';


//const QuickFilter = translate(({ label, translate }) => <Chip style={{ marginBottom: 8 }}>{translate(label)}</Chip>);

// const PostFilter = ({ ...props }) => (
//   <Filter {...props}>
//     <TextInput label="post.list.search" source="q" alwaysOn />
//     <TextInput source="title" defaultValue="Qui tempore rerum et voluptates" />
//     <QuickFilter label="resources.posts.fields.commentable" source="commentable" defaultValue={true} />
//   </Filter>
// );

const titleFieldStyle  = {maxWidth: '20em', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap'};
export const GroupList = ({...props}) => (
  <List {...props}>
    <Responsive
      small={
        <SimpleList
          primaryText={record => record.Name}
          secondaryText={record => record.Description}
        />
      }
      medium={
        <Datagrid>
          <TextField source="id"/>
          <TextField source="Name" style={titleFieldStyle}/>
          <TextField source="Description" style={{fontStyle: 'italic'}}/>
          <TextField source="EmployeeIds" label="Identifiers of employees" />
          <EditButton />
          <ShowButton />
        </Datagrid>
      }
    />
  </List>
);

const GroupTitle = translate(({record, translate}) => {
  return <span>Group</span>;
});

export const GroupCreate = ({...props}) => (
  <Create {...props}>
    <SimpleForm validate={(values) => {
      const errors = {};
      ['Name', 'Description'].forEach((field) => {
        if (!values[field]) {
          errors[field] = ['Required field'];
        }
      });

      return errors;
    }}>
      <TextInput source="Name"/>
      <LongTextInput source="Description" options={{multiLine: true}}/>
      <TextInput source="EmployeeIds" label="Identifiers of employees" />
    </SimpleForm>
  </Create>
);

export const GroupEdit = ({...props}) => (
  <Edit title={<GroupTitle />} {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <TextInput source="Name" validate={required}/>
      <LongTextInput source="Description" validate={required}/>
      <TextInput source="EmployeeIds" label="Identifiers of employees" />
    </SimpleForm>
  </Edit>
);

export const GroupShow = ({...props}) => (
  <Show title={<GroupTitle />} {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="Name"/>
      <TextField source="Description"/>
      <TextField source="EmployeeIds" label="Identifiers of employees" />
    </SimpleShowLayout>
  </Show>
);
