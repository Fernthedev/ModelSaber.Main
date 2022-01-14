import { ModelSaberQuery } from "./graphqlTypes";
type StringNumber = string | number;

export interface GQLError {
  message: string;
  locations: Location[];
  path: StringNumber[];
  extensions: Extensions;
}

export interface Location {
  line: number;
  column: number;
}

export interface Extensions {
  code: string;
  codes: string[];
}

export interface GQLReturn {
  data: ModelSaberQuery;
  errors: GQLError[];
}