import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withRouterConfig } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { CalendarCheck2, LucideAngularModule, X , LayoutGrid, Menu, Home, Users, Settings, Folder, ListTodo, Loader2, ChevronDown, ChevronUp, ClipboardList, Trash, Plus, Edit, NotebookTabs, SlidersHorizontal, ListRestart } from 'lucide-angular';
import { withFetch } from '@angular/common/http';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes, withRouterConfig({onSameUrlNavigation: 'reload'})), provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch()),
    importProvidersFrom(LucideAngularModule.pick({ CalendarCheck2, LayoutGrid, Menu, X, Home, Users, Settings, Folder, ListTodo, Loader2, ChevronDown, ChevronUp, ClipboardList, Trash, Plus , Edit , NotebookTabs, SlidersHorizontal, ListRestart   }))
  ]
};
