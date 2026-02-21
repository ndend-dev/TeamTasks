export interface ConfigColumns {
  key: string;
  label: string;
  visible?: boolean;
  type? : 'text' | 'route' | 'action' | 'date' | 'number';
  id?: string;
  format?: string; 
  actionIcon?: string;
  actionRoute?: string;
  action?: (row: any) => void;
}
