import {FacultyRank} from './FacultyRank'
import IServerGroupStructure from './IServerGroupStructure'
import SectionContent from './../../models/BusinessObjects/Form/SectionContent'
import ReviewComment from './../../models/BusinessObjects/Form/ReviewComment'
import FormState from './../../models/BusinessObjects/Form/FormState'
import { SpotScoreSection } from '../../models/BusinessObjects/Form/SpotScoreSection'
export default interface IServerFormStructure{
    formId: string,
    facultyId: string,
    reviewerId: string,
    facultyContent: Array<SectionContent>,
    reviewContent: Array<ReviewComment>,
    formLevelComment: string,
    state: FormState
    scores: SpotScoreSection;
}