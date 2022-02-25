import {FacultyRank} from './FacultyRank'
import FormState from './../../models/BusinessObjects/Form/FormState'
export default interface IServerFormStub{
    formId: string,
    facultyId: string,
    state: FormState,
    formYear: string,
    rank: FacultyRank;
}