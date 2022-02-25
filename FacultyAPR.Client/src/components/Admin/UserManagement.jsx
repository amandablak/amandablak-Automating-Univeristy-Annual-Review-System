import {Container, Row, Col} from 'react-bootstrap'
import  Users  from './DisplayUsers';
import UserUploadModal from './UserUploadModal'
import ScoresUploadModal from './ScoresUploadModal'
import CreateForm from './CreateForm'
import UserModalForm from './UserModal'
import './management.css'

const UserManagement  = () => {
    return (
      <div className = "user-management">
      <Container>
      <Row>
      <Col>
      <div className="user-management-child">
      <UserUploadModal/>
      </div>
      <div className="user-management-child">
      <ScoresUploadModal/>
      </div>
      <div className="user-management-child">
      <CreateForm/>
      </div>
      <div className="user-management-child">
        <UserModalForm className = "new-user" buttonLabel="Add New User" addUserToState={UserModalForm.addUserToState}/>
      </div>


      </Col>
      <Col xs={9} >
      <div className="user-table">
      <Users />
      </div>
      </Col>
      </Row>
      </Container>
      </div>    
    )
  
}
export default UserManagement;
