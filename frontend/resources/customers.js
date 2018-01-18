import React from 'react';
import {
  AutocompleteInput,
  Create,
  DateField,
  DateInput,
  DisabledInput,
  Edit,
  EditButton,
  Filter,
  List,
  LongTextInput,
  ReferenceField,
  ReferenceInput,
  Responsive,
  SelectInput,
  SimpleList,
  SimpleForm,
  ShowButton,
  SimpleShowLayout,
  Show,
  TextField,
  TextInput,
  minLength,
  maxLength,
  translate,
} from '../src/index';
import PersonIcon from 'material-ui/svg-icons/social/person';
import Avatar from 'material-ui/Avatar';
import {Card, CardActions, CardHeader, CardText} from 'material-ui/Card';
// import FlatButton from 'material-ui/FlatButton';
// import {Toolbar, ToolbarGroup} from 'material-ui/Toolbar';
// import ChevronLeft from 'material-ui/svg-icons/navigation/chevron-left';
// import ChevronRight from 'material-ui/svg-icons/navigation/chevron-right';
import CommentIcon from 'material-ui/svg-icons/communication/chat-bubble';
export {CommentIcon as CustomerIcon }

// const CommentFilter = ({...props}) => (
//   <Filter {...props}>
//     <ReferenceInput source="post_id" reference="posts">
//       <SelectInput optionText="title"/>
//     </ReferenceInput>
//   </Filter>
// );
//
// const CommentPagination = translate(({page, perPage, total, setPage, translate}) => {
//   const nbPages = Math.ceil(total / perPage) || 1;
//   return (
//     nbPages > 1 &&
//     <Toolbar>
//       <ToolbarGroup>
//         {page > 1 &&
//         <FlatButton primary key="prev" label={translate('aor.navigation.prev')} icon={<ChevronLeft />}
//                     onClick={() => setPage(page - 1)}/>
//         }
//         {page !== nbPages &&
//         <FlatButton primary key="next" label={translate('aor.navigation.next')} icon={<ChevronRight />}
//                     onClick={() => setPage(page + 1)} labelPosition="before"/>
//         }
//       </ToolbarGroup>
//     </Toolbar>
//   );
// });

const cardStyle = {
  width        : 300,
  minHeight    : 300,
  margin       : '0.5em',
  display      : 'inline-block',
  verticalAlign: 'top',
};

const CustomerGrid = translate(({ids, data, basePath, translate}) => {
  return (
    <div style={{margin: '1em'}}>
      {ids.map(id =>
        <Card key={id} style={cardStyle}>
          <CardHeader
            title={<TextField record={data[id]} source="FullName"/>}
            avatar={<Avatar icon={<PersonIcon />}/>}
          />
          <CardText>
            <TextField record={data[id]} source="Phone"/>
          </CardText>
          <CardText>
            <TextField record={data[id]} source="CompanyName"/>
          </CardText>
          <CardText>
            <TextField record={data[id]} source="City"/>
          </CardText>
          <CardText>
            <TextField record={data[id]} source="Address"/>
          </CardText>
          <CardActions style={{textAlign: 'right'}}>
            <EditButton resource="customers" basePath={basePath} record={data[id]}/>
            <ShowButton resource="customers" basePath={basePath} record={data[id]}/>
          </CardActions>
        </Card>,
      )}
    </div>
  )
});

CustomerGrid.defaultProps = {
  data: {},
  ids : [],
};

const CustomerMobileList = (props) => (
  <SimpleList
    primaryText={record => record.FullName}
    secondaryText={record => record.Phone}
    secondaryTextLines={1}
    leftAvatar={() => <Avatar icon={<PersonIcon />}/>}
    {...props}
  />
);

export const CustomerList = ({...props}) => (
  <List {...props}>
    <Responsive small={<CustomerMobileList />} medium={<CustomerGrid />}/>
  </List>
);

export const CustomerEdit = ({...props}) => (
  <Edit {...props}>
    <SimpleForm>
      <DisabledInput source="id"/>
      <TextInput source="FullName" validate={minLength(10)}/>
      <TextInput source="CompanyName" validate={minLength(10)}/>
      <TextInput source="Phone" validate={minLength(10)}/>
      <TextInput source="City" validate={[minLength(4), maxLength(15)]}/>
      <LongTextInput source="Address" validate={minLength(10)}/>
    </SimpleForm>
  </Edit>
);

export const CustomerShow = ({...props}) => (
  <Show {...props}>
    <SimpleShowLayout>
      <TextField source="id"/>
      <TextField source="FullName"/>
      <TextField source="CompanyName"/>
      <TextField source="Phone"/>
      <TextField source="City"/>
      <TextField source="Address"/>
    </SimpleShowLayout>
  </Show>
);

export const CustomerCreate = ({...props}) => (
  <Create {...props} defaultValues={{created_at: new Date()}}>
    <SimpleForm>
      <TextInput source="FullName" validate={minLength(10)}/>
      <TextInput source="City" validate={minLength(4)}/>
      <LongTextInput source="Address" validate={minLength(10)}/>
    </SimpleForm>
  </Create>
);
