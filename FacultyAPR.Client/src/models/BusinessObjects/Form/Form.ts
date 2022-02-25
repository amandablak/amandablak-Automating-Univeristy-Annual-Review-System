import FormState from "./FormState"
import Group  from "./Group";
import {SpotScoreSection} from "./SpotScoreSection"
import { FacultyRank } from "../../../common/types/FacultyRank";

type Form = {
    formId: string;
    facultyId: string;
    reviewerId: string;
    groups: Array<Group>;
    comment: string;
    spotScoreSection: SpotScoreSection;
    state: FormState;
    year: string;
    rank: FacultyRank;
}

export default Form