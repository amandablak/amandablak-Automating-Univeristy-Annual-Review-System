import SectionType from "./SectionType"

type SectionStructure = {
    sectionTitle: string;
    sectionDescription: string;
    sectionType: SectionType;
    sectionId: string;
    groupId: string;
    options: Array<string>;
    modified: string;
    orderIndex: number;
}

export default SectionStructure