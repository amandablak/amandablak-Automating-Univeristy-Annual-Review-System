import {FacultyRank} from './FacultyRank'
import IServerGroupStructure from './IServerGroupStructure'
export default interface IServerFormStructure{
    formId: string,
    groups: Array<IServerGroupStructure>;
    formYear: string
    rank: FacultyRank;
}