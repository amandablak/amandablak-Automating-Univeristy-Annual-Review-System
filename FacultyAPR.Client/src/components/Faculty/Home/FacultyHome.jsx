import {useState, useEffect} from 'react'
import {Tab, Row, Nav, Col, Container} from 'react-bootstrap'
import {DisplayForms} from '../../common/DisplayForms'
import {GetFormStubs, GetReviewerByFaculty} from '../../../api/Routing'
import Button from 'react-bootstrap/Button'
import {createForm} from '../../../redux/form/formActions'
import {connect} from 'react-redux'
import {CreateSampleForm, CreateNewForm} from '../../../common/helpers/form'
import {GetFormStructureByClassification} from '../../../api/Routing'
import {useHistory} from 'react-router-dom'
import Toast from 'react-bootstrap/Toast'
import ToastHeader from 'react-bootstrap/ToastHeader'
import ToastBody from 'react-bootstrap/ToastHeader'
import './facultyhome.css'

const FacultyHome = ({createForm, user, isAuthenticated}) => {
    var history = useHistory();
    if (!isAuthenticated)
    {
        history.push("/Login")
    }
    const [formStubs, setFormStubs] = useState([]);
    useEffect(() => {
        async function getFormStubs() {
            try
            {
                const stubs = await GetFormStubs(user.id);
                setFormStubs(stubs.data ?? []);
            } 
            catch(_) {}
        }
        getFormStubs();
    }, [])
    var history = useHistory();

    var currentYear = new Date().getFullYear();
    var history = useHistory();
    var currentYearFormCreated = false;
    for(var i = 0; i < formStubs.length; i++){
        if (formStubs[i].formYear === currentYear){
            currentYearFormCreated = true;
        }
    }

    return (
      <Container>
        <div className = "tabs-forms">
        <Tab.Container  defaultActiveKey="InProgress">
            <Row >
                <Col sm={3}>
                    <Nav  variant="pills" className="flex-column">
                        <Nav.Item >
                            <Nav.Link  className = "tab-button" eventKey="InProgress">In Progress</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link className = "tab-button" eventKey="InReview">In Review</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link className = "tab-button" eventKey="FacultyAcknowledge">Faculty Acknowledge</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link className = "tab-button" eventKey="ToBeSigned">To Be Signed</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link className = "tab-button" eventKey="Completed">Completed</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                        <Nav.Link className = "tab-button" eventKey="Submitted">Submitted to Dean</Nav.Link>
                    </Nav.Item>
                    </Nav>
                    
                </Col>
                <Col sm={9}>
                    <Tab.Content className = "form-table">
                        <Tab.Pane eventKey="InProgress" title="In Progress">
                            <DisplayForms isReadyOnly={false} formState="Draft" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="InReview" title="In Review">
                            <DisplayForms isReadyOnly={true} formState="Review" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="FacultyAcknowledge" title="Faculty Acknowledge">
                            <DisplayForms isReadyOnly={false} formState="FacultyAck" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="ToBeSigned" title="To be Signed">
                            <DisplayForms isReadyOnly={false} formState="ToBeSigned" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="Completed" title="Completed">
                            <DisplayForms isReadyOnly={true} formState="Completed" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                        <Tab.Pane eventKey="Submitted" title = "Submitted">
                            <DisplayForms  formState="Submitted" formStubs={formStubs} createForm={createForm} user={user}/>
                        </Tab.Pane>
                    </Tab.Content>
                    {renderCreateButton(currentYearFormCreated || formStubs === [], currentYear, createForm, "Professor", history, user)}
                    {renderDevFrontend(currentYearFormCreated || formStubs === [], createForm, history, user)}
                </Col>
            </Row>
            {(formStubs.filter(formStub => formStub.state === "FacultyAck").length != 0)
                        ?<Notification/>
                        : null
                        }
                        {(formStubs.filter(formStub => formStub.state === "ToBeSigned").length != 0)
                        ?<Notification1/>
                        : null
                        }
        </Tab.Container>
        </div>
        </Container>
    )
}

  function Notification() {
    const [showA, setShowA] = useState(true);
    const [showB, setShowB] = useState(true);
  
    const toggleShowA = () => setShowA(!showA);
    const toggleShowB = () => setShowB(!showB);
  
    return (
      <Row>
        <Col xs={6}>
          <Toast show={showA} onClose={toggleShowA}>
            <Toast.Header>
              <img
                src="holder.js/20x20?text=%20"
                className="rounded mr-2"
                alt=""
              />
              <strong className="mr-auto">Reminder</strong>
            </Toast.Header>
            <Toast.Body>A form has been sent back to Faculty Acknowledgement</Toast.Body>
          </Toast>
        </Col>
        <Col xs={6}>
          <Button onClick={toggleShowA}>
            Show Reminder
          </Button>
        </Col>
      </Row>
    );
  }
  
  function Notification1() {
    const [showA, setShowA] = useState(true);
    const [showB, setShowB] = useState(true);
  
    const toggleShowA = () => setShowA(!showA);
    const toggleShowB = () => setShowB(!showB);
  
    return (
      <Row>
        <Col xs={6}>
          <Toast show={showA} onClose={toggleShowA}>
            <Toast.Header>
              <img
                src="holder.js/20x20?text=%20"
                className="rounded mr-2"
                alt=""
              />
              <strong className="mr-auto">Reminder</strong>
            </Toast.Header>
            <Toast.Body>A form is waiting to be signed</Toast.Body>
          </Toast>
        </Col>
        <Col xs={6}>
          <Button onClick={toggleShowA}>
            Show Reminder
          </Button>
        </Col>
      </Row>
    );
  }

const renderCreateButton = (currentYearFormExists, currentYear, createForm, rank, history, user) =>
{
    return ( 
        currentYearFormExists 
            ? null 
            : <Button className="mt-2 ml-2" variant="info" onClick={()  => { 
                createFormOnClick(createForm, currentYear, rank, user)
                    .then(() => window.location.reload())
                    .catch(() => {})
                }} >Start Form for {currentYear}</Button>
        )
}

const renderDevFrontend = (currentYearFormExists, createForm, history, user) =>
{
    return ( 
        currentYearFormExists
            ? null
            : <Button onClick={()  => {
                createDevFormOnclick(createForm, user)
                .then(() => history.push("/form"))
                .catch(() => {})
            }} className="mt-2 ml-2" variant="info">Dev Form from helpers/from.tsx</Button>
        )
}
const createFormOnClick = async (createForm, year, rank, user) => {
    var facultyId = user.id;
    var reviewerIdPromise = await GetReviewerByFaculty(facultyId, user.userType);
    var resp = await GetFormStructureByClassification(year, rank);
    await CreateNewForm(reviewerIdPromise.data, facultyId, createForm, resp.data);

}

const createDevFormOnclick = async (createForm, user) => {
    CreateNewForm("1e06676a-08f2-4151-85ca-ce5f490ce77d", user.id, createForm, CreateSampleForm())
}

const mapStateToProps = state => {
    const user = state.user;
    return {user: user, isAuthenticated: state.auth.isAuthenticated};
  };

export default connect(mapStateToProps, {createForm})(FacultyHome)