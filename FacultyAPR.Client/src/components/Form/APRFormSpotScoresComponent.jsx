import {APRFormSpotScoresTable} from './APRFormSpotScoresTable'
import {FacultyReviewComment} from './FacultyReviewComment'
import Form from 'react-bootstrap/Form'
import {updateReviewComment} from '../../redux/form/formActions'
import {FormTextInput} from './FormTextInput'

export const APRFormSpotScoresComponent = ({spotScoreData, state, user, updateFacultyComment}) => {
    const isReadonly = (state != "Draft" || user.userType != "Faculty")
    return (
        <Form.Group>
            <Form.Label>Spot Scores</Form.Label>
            <APRFormSpotScoresTable scores={spotScoreData.scores} />
            <Form.Text id="Description">Please reflect on the scores above</Form.Text>
            <FormTextInput onChange = {(facultyComment) => {updateFacultyComment(facultyComment)}} content={spotScoreData.facultyComment} isReadonly = {isReadonly}/>
            <FacultyReviewComment state={state} user={user} onUpdate={(reviewComment) => updateReviewComment(reviewComment)}/>
        </ Form.Group>
    );
}