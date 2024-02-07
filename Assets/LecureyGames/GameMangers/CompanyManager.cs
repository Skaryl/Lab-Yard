using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class CompanyManager : MonoBehaviour {
        [SerializeField] private List<CompanySO> companiesSO = new();
        [SerializeField] private readonly List<string> companyNames = new();

        private List<Company> companies;
        public List<Company> Companies => companies;
        public Company Company(string name) => companies.Find(c => c.Name == name);

        private void Awake() {
            companies = new();
            foreach (CompanySO companySO in companiesSO) {
                Company company = new(companySO);
                companyNames.Add(companySO.name);
                companies.Add(company);
            }
        }

        public void AddCompany(Company company) => companies.Add(company);
        public void RemoveCompany(Company company) => companies.Remove(company);
    }
}