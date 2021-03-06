// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  battlesHubsUrl: 'https://localhost:44384/hubs/battles',
  apiTrainersUrl: 'https://localhost:44338/api/v1',
  apiPokensUrl: 'https://localhost:44379/api/v1/pokemons',
  apiTrainingUrl: 'https://localhost:44369/api/v1/trainers',
  apiArenaUrl: {
    read: 'https://localhost:44394/api/v1',
    write: 'https://localhost:44384/api/v1'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
